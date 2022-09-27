namespace RejoinToolLib.Model;

using System.Linq;

public record struct Token (string RawToken) {
	public string WorldId => RawToken.Split(':')[0];
	public string? InstanceId => RawToken.Split(':').ElementAtOrDefault(1) ?? null;
	public string ToLaunchURI() => $"vrchat://launch?id={RawToken}&shortName=dummyval";
	public string ToWebSiteURI() => "https://vrchat.com/home/launch"
		+ $"?worldId={WorldId}"
		+ $"{(InstanceId == null ? "" : "&instanceId=")}{InstanceId}";
}
