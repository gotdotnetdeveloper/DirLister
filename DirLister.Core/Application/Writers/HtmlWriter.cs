﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sander.DirLister.Core.Application.Writers
{
	internal sealed class HtmlWriter : BaseWriter
	{
		private readonly StringBuilder _sb;
		private readonly string _now;
		private readonly bool _needsFileInfo;


		public HtmlWriter(Configuration configuration) : base(configuration)
		{
			_sb = new StringBuilder();
			_now = DateTimeOffset.Now.ToString(FileDateFormat);
			_needsFileInfo = Configuration.IncludeSize || Configuration.IncludeFileDates || Configuration.IncludeMediaInfo;
		}

		protected internal override string Write(List<FileEntry> entries)
		{
			BuildHtmlHeader();
			var folderList = GroupByFolder(entries).ToList();
			_sb.AppendLine("<body>");
			BuildDirListerHeader(folderList, entries);

			foreach (var folder in folderList)
			{
				if (folder.Any())
					CreateFolder(folder);
			}

			_sb.AppendLine(
				"<footer><strong>DirLister v2</strong>&nbsp;|&nbsp;<a href=\"https://github.com/SanderSade/DirLister/\">Source, updates & support</a></footer>");
			_sb.AppendLine("</body>");
			_sb.AppendLine("</html>");

			var fileName = GetFilename(OutputFormat.Html);
			using (var sw = new StreamWriter(fileName, false, Encoding.UTF8, 2 << 16 /* 128KB*/))
			{
				sw.Write(_sb.ToString());
			}

			return fileName;
		}

		private void CreateFolder(IGrouping<string, FileEntry> folder)
		{
			_sb.AppendLine("<section>");
			_sb.AppendLine("<details open=\"\">");
			//todo: encoding
			_sb.Append($"<summary>{folder.Key}<span>files: {folder.Count()}");
			if (Configuration.IncludeSize)
				_sb.Append($"&nbsp;({Utils.ReadableSize(folder.Sum(x => x.Size))})");

			_sb.AppendLine("</span></summary>");
			_sb.AppendLine("<ul>");
			foreach (var entry in folder)
			{
				_sb.AppendLine($"<li>{entry.Filename}{GetFileDetails(entry)}</li>");
			}

			_sb.AppendLine("</ul>");
			_sb.AppendLine("</details>");
			_sb.AppendLine("</section>");
		}

		private string GetFileDetails(FileEntry entry)
		{
			string GetMediaInfo<T>(string name, T value)
			{
				return value == null || value.Equals(default(T))
					? string.Empty
					: FormattableString.Invariant($" {name}: {value.ToString()},");
			}

			if (!_needsFileInfo)
				return string.Empty;

			var sb = new StringBuilder("<span>");
			if (Configuration.IncludeSize)
				sb.Append(entry.ReadableSize);

			if (Configuration.IncludeFileDates)
				sb.Append($"&nbsp;|&nbsp;created: {entry.Created.ToString(FileDateFormat)}, modified: {entry.Modified.ToString(FileDateFormat)}");

			if (Configuration.IncludeMediaInfo && entry.MediaInfo != null)
			{
				sb.Append("&nbsp;|&nbsp;");
				sb.Append($"{entry.MediaInfo.MediaType}");
				if (entry.MediaInfo.Duration != TimeSpan.Zero)
					sb.Append($" {FormatDuration(entry.MediaInfo.Duration)},");

				if (entry.MediaInfo.Height > 0)
					sb.Append($" {entry.MediaInfo.Height}x{entry.MediaInfo.Width},");

				sb.Append(GetMediaInfo("bpp", entry.MediaInfo.BitsPerPixel));

				if (entry.MediaInfo.AudioBitRate != 0 || entry.MediaInfo.AudioSampleRate != 0 ||
				    entry.MediaInfo.AudioChannels != 0)
				{
					sb.Remove(sb.Length - 1, 1);
					sb.Append("&nbsp;|&nbsp;audio");
					sb.Append(GetMediaInfo("bitrate", entry.MediaInfo.AudioBitRate));
					sb.Append(GetMediaInfo("channels", entry.MediaInfo.AudioChannels));

					if (entry.MediaInfo.AudioSampleRate > 0)
						sb.Append(GetMediaInfo("sample rate", entry.MediaInfo.AudioSampleRate / 1000f));
				}




				sb.Remove(sb.Length - 1, 1);//remove trailing comma
			}

			sb.Append("</span>");

			return sb.ToString();
		}


		/// <summary>
		/// Header with directory information
		/// </summary>
		/// <param name="folderList"></param>
		/// <param name="entries"></param>
		private void BuildDirListerHeader(List<IGrouping<string, FileEntry>> folderList, List<FileEntry> entries)
		{
			_sb.AppendLine("<aside>");
			_sb.Append(
				"<a href=\"javascript:void(0)\" onclick=\"var els = document.getElementsByTagName('details');for (var i=0; i < els.length; i++) { els[i].setAttribute('open', 'open');}\">open all</a>&nbsp;|&nbsp;");
			_sb.AppendLine(
				"<a href=\"javascript:void(0)\" onclick=\"var els = document.getElementsByTagName('details');for (var i=0; i < els.length; i++) { els[i].removeAttribute('open');}\">collapse all</a>");
			_sb.AppendLine("</aside>");
			_sb.AppendLine("<header>");

			_sb.AppendLine($"<h2>Directory listing {_now}</h2>");
			_sb.AppendLine("<ul>");

			foreach (var folder in Configuration.InputFolders)
			{
				var files = folderList.Where(x => x.Key.StartsWith(folder, StringComparison.Ordinal)).SelectMany(x => x.ToList()).ToList();
				_sb.AppendLine(
					$"<li>{folder} <span>files: {files.Count}, size: {Utils.ReadableSize(files.Sum(x => x.Size))}</span></li>");
			}
			_sb.AppendLine("</ul>");

			if (Configuration.InputFolders.Count > 1)
			{
				_sb.AppendLine($"<strong>Total: {entries.Count} files, {Utils.ReadableSize(entries.Sum(x => x.Size))}");
			}
			_sb.AppendLine("</header>");
		}

		/// <summary>
		/// Standard HTML header, including CSS
		/// </summary>
		private void BuildHtmlHeader()
		{
			_sb.AppendLine($@"<!doctype html>
<html>
<head>
  <meta charset=""utf-8"">
  <title>DirLister&nbsp;{_now}</title>
  <meta name=""generator"" content=""DirLister v2"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"">");

			string css;
			if (string.IsNullOrWhiteSpace(Configuration.CssContent))
			{
				var assembly = Assembly.GetExecutingAssembly();
				var resourceName = "Sander.DirLister.Core.Application.Writers.Default.css";
				using (var stream = assembly.GetManifestResourceStream(resourceName))
				{
					Debug.Assert(stream != null, nameof(stream) + " != null");
					using (var reader = new StreamReader(stream))
					{
						css = reader.ReadToEnd();
					}
				}
			}
			else css = Configuration.CssContent;

			_sb.AppendLine("<style>");
			_sb.AppendLine(css);
			_sb.AppendLine("</style>");
			_sb.AppendLine("</head>");
		}
	}
}
