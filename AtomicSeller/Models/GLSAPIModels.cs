using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GLSAPI.Models
{
    //common class
    public class ResponseHeader
    {
        public string RequestStatus { get; set; }

        public string ReturnCode { get; set; }

        public string ReturnMessage { get; set; }

        public string LanguageCode { get; set; }
    }


    //Shipment Class
    public class ShipmentRequest
    {
        public string shipperId { get; set; }
        public string shipmentDate { get; set; }
        public List<string> references { get; set; }
        public addresses addresses { get; set; }
        public List<parcel> parcels { get; set; }
    }

    public class parcel
    {
        public float weight { get; set; }
        public List<string> references { get; set; }
        public string comment { get; set; }
    }

    public class addresses
    {
        public address delivery { get; set; }
        public address alternativeShipper { get; set; }
    }

    public class address
    {
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string name3 { get; set; }
        public string street1 { get; set; }
        public string country { get; set; }
        public string zipCode { get; set; }
        public string city { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
    }

    public class ShipmentResponse
    {
        public ResponseHeader responseHeader { get; set; }
        public ShipmentResponseData responseData { get; set; }
    }
    public class ShipmentResponseData
    {
        public List<string> labels{ get; set; }
        public List<parcelInfo> parcels { get; set; }
        public List<parcelInfo> returns { get; set; }
        public string location { get; set; }
        public string consignmentId { get; set; }
    }

    public class parcelInfo
    {
        public string parcelNumber { get; set; }
        public string trackId { get; set; }
        public string location { get; set; }
    }

}