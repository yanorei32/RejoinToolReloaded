namespace RejoinToolLib.Parser;

// New parser from VRChatRejoinToolCore/RamType0.
//   BSD 2-Clause License
//   Copyright (c) 2022, Ram.Type-0

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using RejoinToolLib.Model;

public static class LogParser {
	public static void GetJoinEvents(List<JoinEvent> joinEvents, ReadOnlySpan<byte> log) {
		var remaining = log;
		var instanceTokenStartIndex = DestinationSetInstanceIdIndexOf(remaining);

		while (instanceTokenStartIndex >= 0) {
			var instanceTokenLength = remaining.Slice(instanceTokenStartIndex).IndexOf((byte) '\n');
			var instanceTokenSpan = remaining.Slice(instanceTokenStartIndex, instanceTokenLength);

			string instanceToken;

			unsafe {
				fixed (byte* ptr = instanceTokenSpan) {
					instanceToken = Encoding.UTF8.GetString(ptr,instanceTokenSpan.Length);
				}
			}

			var previousLineEndIndex = remaining.Slice(0, instanceTokenStartIndex).LastIndexOf((byte) '\n');
			var timeStampStartIndex = previousLineEndIndex >= 0 ? previousLineEndIndex + 1 : 0;
			var timeStampSpan = remaining.Slice(timeStampStartIndex, 19);

			string timeStampString;

			unsafe {
				fixed (byte* ptr = timeStampSpan) {
					timeStampString = Encoding.UTF8.GetString(ptr,timeStampSpan.Length);
				}
			}

			var timeStamp = DateTime.Parse(timeStampString);

			remaining = remaining.Slice(instanceTokenStartIndex + instanceToken.Length);

			var nextInstanceIdStartIndex = DestinationSetInstanceIdIndexOf(remaining);
			var worldNameSearchLength = nextInstanceIdStartIndex >= 0 ? nextInstanceIdStartIndex : remaining.Length;
			var worldNameSearchSpan = remaining.Slice(0, worldNameSearchLength);
			var worldName = ExtractWorldName(worldNameSearchSpan);

			joinEvents.Add(JoinEvent.BuildFromInformation(timeStamp, new(instanceToken), worldName));

			instanceTokenStartIndex = nextInstanceIdStartIndex;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static int DestinationSetInstanceIdIndexOf(ReadOnlySpan<byte> logFragment) {
		ReadOnlySpan<byte> destinationSet = stackalloc byte[] {
			(byte) '[',
			(byte) 'B',
			(byte) 'e',
			(byte) 'h',
			(byte) 'a',
			(byte) 'v',
			(byte) 'i',
			(byte) 'o',
			(byte) 'u',
			(byte) 'r',
			(byte) ']',
			(byte) ' ',
			(byte) 'D',
			(byte) 'e',
			(byte) 's',
			(byte) 't',
			(byte) 'i',
			(byte) 'n',
			(byte) 'a',
			(byte) 't',
			(byte) 'i',
			(byte) 'o',
			(byte) 'n',
			(byte) ' ',
			(byte) 's',
			(byte) 'e',
			(byte) 't',
			(byte) ':',
			(byte) ' ',
			(byte) 'w',
			(byte) 'r',
			(byte) 'l',
			(byte) 'd',
		};

		var scanedBytes = 0;
		var remaining = logFragment;

		while (true) {
			var underbarIndex = remaining.IndexOf((byte) '_'); // '_' is the best single char for searching "wrld_"
			if (underbarIndex < 0) return -1;

			if (remaining.Slice(0, underbarIndex).EndsWith(destinationSet)) {
				var localStartIndex = underbarIndex - 4;
				return scanedBytes + localStartIndex;
			} else {
				scanedBytes += underbarIndex + 1;
				remaining = logFragment.Slice(scanedBytes);
			}
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static string? ExtractWorldName(ReadOnlySpan<byte> logFragment) {
		ReadOnlySpan<byte> enteringRoom = stackalloc byte[] {
			(byte) 'E',
			(byte) 'n',
			(byte) 't',
			(byte) 'e',
			(byte) 'r',
			(byte) 'i',
			(byte) 'n',
			(byte) 'g',
			(byte) ' ',
			(byte) 'R',
			(byte) 'o',
			(byte) 'o',
			(byte) 'm',
			(byte) ':',
			(byte) ' ',
		};

		var enteringRoomStartIndex = logFragment.IndexOf(enteringRoom);
		if (enteringRoomStartIndex < 0) return null;

		var worldNameStartIndex = enteringRoomStartIndex + enteringRoom.Length;
		var worldNameLength = logFragment.Slice(worldNameStartIndex).IndexOf((byte) '\n');
		var worldNameSpan = logFragment.Slice(worldNameStartIndex, worldNameLength);

		unsafe {
			fixed (byte* ptr = worldNameSpan) {
				return Encoding.UTF8.GetString(ptr, worldNameSpan.Length);
			}
		}
	}
}
