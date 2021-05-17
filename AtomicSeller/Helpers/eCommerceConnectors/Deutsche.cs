using AtomicSeller.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web;
using AtomicSeller.ViewModels;
using GLSAPI.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Text;
using GLSAPI.Models;

namespace AtomicSeller.Helpers.eCommerceConnectors
{
    public class GLS
    {
        private static string USERNAME = "250test";
        private static string PASSWORD = "250testpwd";
        private static string CUSTOMERID = "2505400034";
        private static string CONTACTID = "250aaawbn6";
        private static String API_BASE_URL = "https://api-qs.gls-group.eu/public/v1";


        private string SendPostHttpRequest(String url, String jsonParam)
        {
            const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            ServicePointManager.SecurityProtocol = Tls12;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(USERNAME + ":" + PASSWORD));
            httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);

            using (StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                writer.WriteLine(jsonParam);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var result = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }


        public ShipmentResponse GLS_SendParcelData()
        {
            ShipmentResponse _shipmentResult = new ShipmentResponse();
            ResponseHeader _ResponseHeader = new ResponseHeader();
            _ResponseHeader.LanguageCode = "En";
            _ResponseHeader.RequestStatus = "Ok";
            _ResponseHeader.ReturnCode = "AS0000";
            _ResponseHeader.ReturnMessage = "";
            _shipmentResult.responseHeader = _ResponseHeader;

            ShipmentRequest _shipmentRequest = new ShipmentRequest();
            _shipmentRequest.shipperId = CUSTOMERID + " " + CONTACTID;
            _shipmentRequest.shipmentDate = "2021-12-04";
            _shipmentRequest.references = new List<string>();
            _shipmentRequest.references.Add("test references");
            address _deliveryAddress = new address();
            address _alternativeShipperAddress = new address();
            _shipmentRequest.addresses = new addresses();
            _shipmentRequest.addresses.delivery = _deliveryAddress;
            _shipmentRequest.addresses.alternativeShipper = _alternativeShipperAddress;
            _deliveryAddress.name1 = "Name or Company Name";
            _deliveryAddress.name2 = "Complementary address";
            _deliveryAddress.name3 = "Complementary address";
            _deliveryAddress.street1 = "Main address";
            _deliveryAddress.country = "FR";
            _deliveryAddress.zipCode = "60000";
            _deliveryAddress.city = "Compiegne";
            _deliveryAddress.contact = "contact name";
            _deliveryAddress.email = "email@gls-fance.fr";
            _deliveryAddress.phone = "0351120000";
            _deliveryAddress.mobile = "0616840012";

            _alternativeShipperAddress.name1 = "Name or Company Name";
            _alternativeShipperAddress.name2 = "Complementary address";
            _alternativeShipperAddress.name3 = "Complementary address";
            _alternativeShipperAddress.street1 = "Main address";
            _alternativeShipperAddress.country = "FR";
            _alternativeShipperAddress.zipCode = "21200";
            _alternativeShipperAddress.city = "Beaune";

            parcel _parcel = new parcel();
            _shipmentRequest.parcels = new List<parcel>();
            _shipmentRequest.parcels.Add(_parcel);
            _parcel.weight = (float)2.5;
            _parcel.references = new List<string>();
            _parcel.references.Add("parcel specific reference");
            _parcel.comment = "test Comment";

            string jsonParam = string.Empty;
            jsonParam = JsonConvert.SerializeObject(_shipmentRequest, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }).ToString();

            string strShipmentResult = string.Empty;
            string Shipment_API_URL = API_BASE_URL + "/shipments";
            try
            {
                strShipmentResult = new GLS().SendPostHttpRequest(Shipment_API_URL, jsonParam);
                ShipmentResponseData shipmentData = JsonConvert.DeserializeObject<ShipmentResponseData>(strShipmentResult);

                _shipmentResult.responseData = shipmentData;
            }
            catch (Exception ex)
            {
                _ResponseHeader.LanguageCode = "En";
                _ResponseHeader.RequestStatus = "Error";
                _ResponseHeader.ReturnCode = "WZ0"; ;
                _ResponseHeader.ReturnMessage = ex.Message;
                _shipmentResult.responseHeader = _ResponseHeader;
            }

            return _shipmentResult;
        }


        public List<string> GLS_GetShipmentLabel()
        {
            List<string> lstLabelResult = new List<string>();
            ShipmentResponse _shipmentResult = new ShipmentResponse();
            ResponseHeader _ResponseHeader = new ResponseHeader();
            _ResponseHeader.LanguageCode = "En";
            _ResponseHeader.RequestStatus = "Ok";
            _ResponseHeader.ReturnCode = "AS0000";
            _ResponseHeader.ReturnMessage = "";
            _shipmentResult.responseHeader = _ResponseHeader;

            ShipmentRequest _shipmentRequest = new ShipmentRequest();
            _shipmentRequest.shipperId = CUSTOMERID + " " + CONTACTID;
            _shipmentRequest.shipmentDate = "2021-12-04";
            _shipmentRequest.references = new List<string>();
            _shipmentRequest.references.Add("test references");
            address _deliveryAddress = new address();
            address _alternativeShipperAddress = new address();
            _shipmentRequest.addresses = new addresses();
            _shipmentRequest.addresses.delivery = _deliveryAddress;
            _shipmentRequest.addresses.alternativeShipper = _alternativeShipperAddress;
            _deliveryAddress.name1 = "Name or Company Name";
            _deliveryAddress.name2 = "Complementary address";
            _deliveryAddress.name3 = "Complementary address";
            _deliveryAddress.street1 = "Main address";
            _deliveryAddress.country = "FR";
            _deliveryAddress.zipCode = "60000";
            _deliveryAddress.city = "Compiegne";
            _deliveryAddress.contact = "contact name";
            _deliveryAddress.email = "email@gls-fance.fr";
            _deliveryAddress.phone = "0351120000";
            _deliveryAddress.mobile = "0616840012";

            _alternativeShipperAddress.name1 = "Name or Company Name";
            _alternativeShipperAddress.name2 = "Complementary address";
            _alternativeShipperAddress.name3 = "Complementary address";
            _alternativeShipperAddress.street1 = "Main address";
            _alternativeShipperAddress.country = "FR";
            _alternativeShipperAddress.zipCode = "21200";
            _alternativeShipperAddress.city = "Beaune";

            parcel _parcel = new parcel();
            _shipmentRequest.parcels = new List<parcel>();
            _shipmentRequest.parcels.Add(_parcel);
            _parcel.weight = (float)2.5;
            _parcel.references = new List<string>();
            _parcel.references.Add("parcel specific reference");
            _parcel.comment = "test Comment";

            string jsonParam = string.Empty;
            jsonParam = JsonConvert.SerializeObject(_shipmentRequest, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }).ToString();

            string strShipmentResult = string.Empty;
            string Shipment_API_URL = API_BASE_URL + "/shipments";
            try
            {
                strShipmentResult = new GLS().SendPostHttpRequest(Shipment_API_URL, jsonParam);
                ShipmentResponseData shipmentData = JsonConvert.DeserializeObject<ShipmentResponseData>(strShipmentResult);

                _shipmentResult.responseData = shipmentData;

                lstLabelResult = shipmentData.labels;
            }
            catch (Exception ex)
            {
                _ResponseHeader.LanguageCode = "En";
                _ResponseHeader.RequestStatus = "Error";
                _ResponseHeader.ReturnCode = "WZ0"; ;
                _ResponseHeader.ReturnMessage = ex.Message;
                _shipmentResult.responseHeader = _ResponseHeader;
            }

            return lstLabelResult;
        }


        public List<string> GLS_GetTrackingNumber()
        {
            List<string> lstTrackingNumder = new List<string>();
            ShipmentResponse _shipmentResult = new ShipmentResponse();
            ResponseHeader _ResponseHeader = new ResponseHeader();
            _ResponseHeader.LanguageCode = "En";
            _ResponseHeader.RequestStatus = "Ok";
            _ResponseHeader.ReturnCode = "AS0000";
            _ResponseHeader.ReturnMessage = "";
            _shipmentResult.responseHeader = _ResponseHeader;

            ShipmentRequest _shipmentRequest = new ShipmentRequest();
            _shipmentRequest.shipperId = CUSTOMERID + " " + CONTACTID;
            _shipmentRequest.shipmentDate = "2021-12-04";
            _shipmentRequest.references = new List<string>();
            _shipmentRequest.references.Add("test references");
            address _deliveryAddress = new address();
            address _alternativeShipperAddress = new address();
            _shipmentRequest.addresses = new addresses();
            _shipmentRequest.addresses.delivery = _deliveryAddress;
            _shipmentRequest.addresses.alternativeShipper = _alternativeShipperAddress;
            _deliveryAddress.name1 = "Name or Company Name";
            _deliveryAddress.name2 = "Complementary address";
            _deliveryAddress.name3 = "Complementary address";
            _deliveryAddress.street1 = "Main address";
            _deliveryAddress.country = "FR";
            _deliveryAddress.zipCode = "60000";
            _deliveryAddress.city = "Compiegne";
            _deliveryAddress.contact = "contact name";
            _deliveryAddress.email = "email@gls-fance.fr";
            _deliveryAddress.phone = "0351120000";
            _deliveryAddress.mobile = "0616840012";

            _alternativeShipperAddress.name1 = "Name or Company Name";
            _alternativeShipperAddress.name2 = "Complementary address";
            _alternativeShipperAddress.name3 = "Complementary address";
            _alternativeShipperAddress.street1 = "Main address";
            _alternativeShipperAddress.country = "FR";
            _alternativeShipperAddress.zipCode = "21200";
            _alternativeShipperAddress.city = "Beaune";

            parcel _parcel = new parcel();
            _shipmentRequest.parcels = new List<parcel>();
            _shipmentRequest.parcels.Add(_parcel);
            _parcel.weight = (float)2.5;
            _parcel.references = new List<string>();
            _parcel.references.Add("parcel specific reference");
            _parcel.comment = "test Comment";

            string jsonParam = string.Empty;
            jsonParam = JsonConvert.SerializeObject(_shipmentRequest, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }).ToString();

            string strShipmentResult = string.Empty;
            string Shipment_API_URL = API_BASE_URL + "/shipments";
            try
            {
                strShipmentResult = new GLS().SendPostHttpRequest(Shipment_API_URL, jsonParam);
                ShipmentResponseData shipmentData = JsonConvert.DeserializeObject<ShipmentResponseData>(strShipmentResult);

                _shipmentResult.responseData = shipmentData;

                foreach (parcelInfo parcelinfo in shipmentData.parcels)
                {
                    lstTrackingNumder.Add(parcelinfo.trackId);
                }
            }
            catch (Exception ex)
            {
                _ResponseHeader.LanguageCode = "En";
                _ResponseHeader.RequestStatus = "Error";
                _ResponseHeader.ReturnCode = "WZ0"; ;
                _ResponseHeader.ReturnMessage = ex.Message;
                _shipmentResult.responseHeader = _ResponseHeader;
            }

            return lstTrackingNumder;
        }
    }
}