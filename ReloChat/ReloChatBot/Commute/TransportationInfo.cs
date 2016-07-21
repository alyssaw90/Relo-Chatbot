using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReloChatBot.Transportation
{
    
    public class TransportationInfo
    {
        public geocoded_waypoints[] geocoded_waypoints { get; set; }
        public route[] routes { get; set; }
        public string status { get; set; }
    }

    public class geocoded_waypoints
    {
        public string geocoder_status { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }

    public class route
    {
        public bounds bounds { get; set; }
        public string copyrights { get; set; }
        public fare fare { get; set; }
        public leg[] legs { get; set; }
        public overview_polyline overview_polyline { get; set; }
        public string summary { get; set; }
        public string[] warnings { get; set; }
        public object[] waypoint_order { get; set; }
    }

    public class bounds
    {
        public northeast northeast { get; set; }
        public southwest southwest { get; set; }
    }

    public class northeast
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class southwest
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class fare
    {
        public string currency { get; set; }
        public string text { get; set; }
        public float value { get; set; }
    }

    public class overview_polyline
    {
        public string points { get; set; }
    }

    public class leg
    {
        public arrival_time arrival_time { get; set; }
        public departure_time departure_time { get; set; }
        public distance distance { get; set; }
        public duration duration { get; set; }
        public string end_address { get; set; }
        public end_location end_location { get; set; }
        public string start_address { get; set; }
        public start_location start_location { get; set; }
        public step[] steps { get; set; }
        public object[] traffic_speed_entry { get; set; }
        public object[] via_waypoint { get; set; }
    }

    public class arrival_time
    {
        public string text { get; set; }
        public string time_zone { get; set; }
        public int value { get; set; }
    }

    public class departure_time
    {
        public string text { get; set; }
        public string time_zone { get; set; }
        public int value { get; set; }
    }

    public class distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class end_location
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class start_location
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class step
    {
        public distance1 distance { get; set; }
        public duration1 duration { get; set; }
        public end_location1 end_location { get; set; }
        public string html_instructions { get; set; }
        public polyline polyline { get; set; }
        public start_location1 start_location { get; set; }
        public step1[] steps { get; set; }
        public string travel_mode { get; set; }
        public transit_details transit_details { get; set; }
    }

    public class distance1
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class duration1
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class end_location1
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class polyline
    {
        public string points { get; set; }
    }

    public class start_location1
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class transit_details
    {
        public arrival_stop arrival_stop { get; set; }
        public arrival_time1 arrival_time { get; set; }
        public departure_stop departure_stop { get; set; }
        public departure_time1 departure_time { get; set; }
        public string headsign { get; set; }
        public line line { get; set; }
        public int num_stops { get; set; }
    }

    public class arrival_stop
    {
        public location location { get; set; }
        public string name { get; set; }
    }

    public class location
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class arrival_time1
    {
        public string text { get; set; }
        public string time_zone { get; set; }
        public int value { get; set; }
    }

    public class departure_stop
    {
        public location1 location { get; set; }
        public string name { get; set; }
    }

    public class location1
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class departure_time1
    {
        public string text { get; set; }
        public string time_zone { get; set; }
        public int value { get; set; }
    }

    public class line
    {
        public agency[] agencies { get; set; }
        public string short_name { get; set; }
        public string url { get; set; }
        public vehicle vehicle { get; set; }
    }

    public class vehicle
    {
        public string icon { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class agency
    {
        public string short_name { get; set; }
        public string phone { get; set; }
        public string url { get; set; }
    }

    public class step1
    {
        public distance2 distance { get; set; }
        public duration2 duration { get; set; }
        public end_location2 end_location { get; set; }
        public polyline1 polyline { get; set; }
        public start_location2 start_location { get; set; }
        public string travel_mode { get; set; }
        public string html_instructions { get; set; }
        public string maneuver { get; set; }
    }

    public class distance2
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class duration2
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class end_location2
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class polyline1
    {
        public string points { get; set; }
    }

    public class start_location2
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

}