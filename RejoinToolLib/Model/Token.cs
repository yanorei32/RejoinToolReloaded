namespace RejoinToolLib.Model;

using System.Linq;

public record struct Token (string Value) {
	public string WorldId => Value.Split(':')[0];
	public string? InstanceId => Value.Split(':').ElementAtOrDefault(1) ?? null;
	public string LaunchURI => $"vrchat://{Value}";
	public string WebSiteURI => "https://vrchat.com/home/launch"
		+ $"?worldId={WorldId}"
		+ $"{(InstanceId == null ? "" : "&instanceId=")}{InstanceId}";
}
