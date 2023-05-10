using System;
using System.Collections.Generic;

namespace OktaEventHookFunctionApp
{
    public class UserAgent
    {
        public string RawUserAgent { get; set; }
        public string Os { get; set; }
        public string Browser { get; set; }
    }

    public class Geolocation
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class GeographicalContext
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public Geolocation Geolocation { get; set; }
    }

    public class IpChain
    {
        public string Ip { get; set; }
        public GeographicalContext GeographicalContext { get; set; }
        public string Version { get; set; }
        public object Source { get; set; }
    }

    public class Client
    {
        public UserAgent UserAgent { get; set; }
        public string Zone { get; set; }
        public string Device { get; set; }
        public object Id { get; set; }
        public string IpAddress { get; set; }
        public GeographicalContext GeographicalContext { get; set; }
        public List<IpChain> IpChain { get; set; }
    }

    public class Actor
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string AlternateId { get; set; }
        public string DisplayName { get; set; }
        public object DetailEntry { get; set; }
    }

    public class Outcome
    {
        public string Result { get; set; }
        public object Reason { get; set; }
    }

    public class Target
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string AlternateId { get; set; }
        public string DisplayName { get; set; }
        public object DetailEntry { get; set; }
    }

    public class Detail
    {
    }

    public class Transaction
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public Detail Detail { get; set; }
    }

    public class DebugData
    {
        public string RequestId { get; set; }
        public string RequestUri { get; set; }
        public string TargetEventHookIds { get; set; }
        public string Url { get; set; }
    }

    public class DebugContext
    {
        public DebugData DebugData { get; set; }
    }

    public class AuthenticationContext
    {
        public object AuthenticationProvider { get; set; }
        public object CredentialProvider { get; set; }
        public object CredentialType { get; set; }
        public object Issuer { get; set; }
        public int AuthenticationStep { get; set; }
        public string ExternalSessionId { get; set; }
        public object Interface { get; set; }
    }

    public class SecurityContext
    {
        public int AsNumber { get; set; }
        public string AsOrg { get; set; }
        public string Isp { get; set; }
        public string Domain { get; set; }
        public bool IsProxy { get; set; }
    }

    public class OktaEvent
    {
        public string Uuid { get; set; }
        public DateTime Published { get; set; }
        public string EventType { get; set; }
        public string Version { get; set; }
        public string DisplayMessage { get; set; }
        public string Severity { get; set; }
        public Client Client { get; set; }
        public object Device { get; set; }
        public Actor Actor { get; set; }
        public Outcome Outcome { get; set; }
        public List<Target> Target { get; set; }
        public Transaction Transaction { get; set; }
        public DebugContext DebugContext { get; set; }
        public string LegacyEventType { get; set; }
        public AuthenticationContext AuthenticationContext { get; set; }
        public SecurityContext SecurityContext { get; set; }
        public object InsertionTimestamp { get; set; }
    }

    public class Data
    {
        public List<OktaEvent> Events { get; set; }
    }

    public class OktaEventHookEvent
    {
        public string EventType { get; set; }
        public string EventTypeVersion { get; set; }
        public string CloudEventsVersion { get; set; }
        public string Source { get; set; }
        public string EventId { get; set; }
        public Data Data { get; set; }
        public DateTime EventTime { get; set; }
        public string ContentType { get; set; }
    }
}
