namespace RejoinToolLib.Model;

using System.Linq;

public record struct InstanceInformation (string WorldId) {
	public string? Name { get; init; } = default!;
	public Permission Permission { get; init; } = default!;
	public User? OwnerId { get; init; } = default!;
	public VRChatServer ActualServer { get; init; } = default!;
	public string? RegionStr { get; init; } = default!;
	public string? Nonce { get; init; } = default!;

	static public InstanceInformation BuildFromToken(Token token) {
		if (token.InstanceId == null)
			return new (token.WorldId) {
				Permission = Permission.Invalid,
				ActualServer = VRChatServer.Invalid,
			};

		var remaining = token.InstanceId.Split('~')!;
		var instanceName = remaining[0];
		Permission permission = Permission.Public;
		VRChatServer actualServer = VRChatServer.USE;
		string? ownerId = null, nonce = null, regionStr = null;

		foreach (var arg in remaining.Skip(1)) {
			var argS = arg.Split('(');
			var argV = argS.ElementAtOrDefault(1)?.Replace(")", string.Empty) ?? null;

			switch (argS?[0]!) {
				case "hidden":
					ownerId = argV;
					permission = Permission.FriendsPlus;
					break;
				case "friends":
					ownerId = argV;
					permission = Permission.Friends;
					break;
				case "private":
					ownerId = argV;
					permission = Permission.InviteOnly;
					break;
				case "canRequestInvite":
					permission = Permission.InvitePlus;
					break;
				case "region":
					regionStr = argV;
					switch (regionStr) {
						case "use": actualServer = VRChatServer.USE; break;
						case "eu": actualServer = VRChatServer.EU; break;
						case "jp": actualServer = VRChatServer.JP; break;
					}
					break;
				case "nonce":
					nonce = argV;
					break;
				default:
					break;
			}
		}

		return new (token.WorldId) {
			Name = instanceName,
			Permission = permission,
			ActualServer = actualServer,
			OwnerId = ownerId == null ? null : new(ownerId),
			Nonce = nonce,
			RegionStr = regionStr,
		};
	}

	public override string ToString() =>
		$"{ActualServer.ToString()}/{Permission.ToString()}/{Name}";
}
