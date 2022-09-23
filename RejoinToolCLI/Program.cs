namespace RejoinToolCLI;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using RejoinToolLib.Model;
using RejoinToolLib.Validator;
using RejoinToolLib.Parser;

class Program {
	static void Main(string[] args) {
		var events = new List<JoinEvent>();

		foreach (var a in args)
			LogParser.GetJoinEvents(events, File.ReadAllBytes(a));

		events = events.OrderBy(e => e.TimeStamp).ToList();

		foreach (var e in events) {
			if (Validator.ValidateInstanceInformation(e.InstanceInformation).Any())
				continue;

			Console.WriteLine(e.WorldName ?? "(!) Failed to Joining");
			var i = e.InstanceInformation;
			Console.WriteLine($"  {i.Permission}:{i.ActualServer.ToString()}:{i.Name}");
			Console.WriteLine(e.TimeStamp.ToString("  yyyy/MM/dd HH:mm:ss"));
			Console.WriteLine("  " + e.Token.ToWebSiteURI());
			Console.WriteLine();
		}
	}
}
