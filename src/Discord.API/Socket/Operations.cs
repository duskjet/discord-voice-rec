namespace Discord.API.Socket
{
    public enum GatewayOperation
    {
        Dispatch = 0,           // dispatches an event
        Heartbeat = 1,          // used for ping checking
        Identify = 2,           // used for client handshake
        StatusUpdate = 3,       // used to update the client status
        VoiceStateUpdate = 4,   // used to join/move/leave voice channels
        VoiceServerPing = 5,    // used for voice ping checking
        Resume = 6,             // used to resume a closed connection
        Reconnect = 7,          // used to tell clients to reconnect to the gateway
        RequestGuildMembers = 8, // used to request guild members
        InvalidSession = 9,     // used to notify client they have an invalid session id
        Hello = 10,             // sent immediately after connecting, contains heartbeat and server debug information
        HeartbackACK = 11       // sent immediately following a client heartbeat that was received
    }

    public enum VoiceOperation
    {

    }
}
