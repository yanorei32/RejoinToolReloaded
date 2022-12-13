namespace RejoinToolLib.Model;

public record struct Owner (string Id) {
	private bool isGroup() => Id.StartsWith("grp");
	public string ToWebSiteURI() => isGroup() ? $"https://vrchat.com/home/group/{Id}" : $"https://vrch.at/{Id}";
}
