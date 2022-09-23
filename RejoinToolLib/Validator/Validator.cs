namespace RejoinToolLib.Validator;

using System.Collections.Generic;
using System.Text.RegularExpressions;
using RejoinToolLib.Validator.Model;
using RejoinToolLib.Model;

public static class Validator {
	static Regex worldIdR = new Regex(@"\Awr?ld_[a-f0-9]{8}-[a-f0-9]{4}-[a-f0-9]{4}-[a-f0-9]{4}-[a-f0-9]{12}\z");
	static Regex nonceR = new Regex(@"\A([0-9A-F]{48}|[0-9A-F]{64}|[a-f0-9]{8}-[a-f0-9]{4}-[0-5][a-f0-9]{3}-[a-b0-9][a-f0-9]{3}-[a-f0-9]{12})\z");
	static Regex instanceNameR = new Regex(@"\A[A-Za-z0-9]+\z");
	static Regex userIdR = new Regex(@"\Ausr_[a-f0-9]{8}-[a-f0-9]{4}-4[a-f0-9]{3}-[a-f0-9]{4}-[a-f0-9]{12}\z");
	static Regex regionStrR = new Regex(@"\A[a-z]{1,3}\z");

	public static ValidationResult<UserProperties>?
		ValidateUser(User u) {
		if (!userIdR.IsMatch(u.Value)) {
			return new ValidationResult<UserProperties>(
				UserProperties.Value,
				"Must be 'usr_' + UUID"
			);
		}

		return null;
	}

	public static List<ValidationResult<InstanceInformationProperties>>
		ValidateInstanceInformation(InstanceInformation i) {
		var list = new List<ValidationResult<InstanceInformationProperties>>();

		if (!worldIdR.IsMatch(i.WorldId)) {
			list.Add(
				new ValidationResult<InstanceInformationProperties>(
					InstanceInformationProperties.WorldId,
					"Must be 'wrld_' + UUID"
				)
			);
		}

		if (i.Name == null) {
			list.Add(
				new ValidationResult<InstanceInformationProperties>(
					InstanceInformationProperties.Name,
					"Required"
				)
			);
		} else {
			if (!instanceNameR.IsMatch(i.Name)) {
				list.Add(
					new ValidationResult<InstanceInformationProperties>(
						InstanceInformationProperties.Name,
						"Must be [A-Za-z0-9]+"
					)
				);
			}
		}

		if (i.RegionStr != null && !regionStrR.IsMatch(i.RegionStr)) {
			list.Add(
				new ValidationResult<InstanceInformationProperties>(
					InstanceInformationProperties.RegionStr,
					"Must be [a-z]{1,3}"
				)
			);
		}

		if (i.OwnerId == null) {
			switch (i.Permission) {
				case Permission.FriendsPlus:
				case Permission.Friends:
				case Permission.InvitePlus:
				case Permission.InviteOnly:
					list.Add(
						new ValidationResult<InstanceInformationProperties>(
							InstanceInformationProperties.OwnerId,
							"Required"
						)
					);
					break;
			}
		} else {
			var r = ValidateUser(i.OwnerId.Value);
			if (r != null) {
				list.Add(
					new ValidationResult<InstanceInformationProperties>(
						InstanceInformationProperties.OwnerId,
						r.Value.Reason
					)
				);
			}
		}

		if (i.Nonce == null) {
			switch (i.Permission) {
				case Permission.InvitePlus:
				case Permission.InviteOnly:
					list.Add(
						new ValidationResult<InstanceInformationProperties>(
							InstanceInformationProperties.OwnerId,
							"Required in Invite*"
						)
					);
					break;
			}
		} else {
			if (i.Permission == Permission.Public) {
				list.Add(
					new ValidationResult<InstanceInformationProperties>(
						InstanceInformationProperties.Nonce,
						"Not allowed in Public"
					)
				);
			} else if (!nonceR.IsMatch(i.Nonce)) {
				list.Add(
					new ValidationResult<InstanceInformationProperties>(
						InstanceInformationProperties.Nonce,
						"Must be UUID or [0-9A-F]{48 or 64}"
					)
				);
			}
		}

		return list;
	}
}
