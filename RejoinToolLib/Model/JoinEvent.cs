namespace RejoinToolLib.Model;

using System;
using System.Globalization;
using RejoinToolLib.Model;

public record JoinEvent() {
	public DateTime TimeStamp { get; init; } = default!;
	public Token Token { get; init; } = default!;
	public string? WorldName { get; init; } = default!;
	public InstanceInformation InstanceInformation { get; init; } = default!;

	static public JoinEvent BuildFromInformation(
		DateTime timeStamp,
		Token token,
		string? worldName
	) {
		return new () {
			Token = token,
			WorldName = worldName,
			TimeStamp = timeStamp,
			InstanceInformation = InstanceInformation.BuildFromToken(token),
		};
	}

	public override string ToString() =>
		TimeStamp.ToString("o", CultureInfo.InvariantCulture)
		+ $" join {WorldName ?? "unknown"}"
		+ $" {Token.WebSiteURI}";
}
