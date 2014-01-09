//******************************************************************************
//* Name          : PxPay.cs
//* Description   : Classes used interact with the PxPay interface using C# .Net 3.5
//* Copyright	  : Direct Payment Solutions 2009(c)
//* Date          : 2009-05-06
//* Version		  : 1.0
//* Author		  : Thomas Treadwell
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Net;
using System.Xml;
using System.IO;
using System.Reflection;

namespace GopalanCinemasBL
{
    
        /// <summary>
        /// Main class for submitting transactions via PxPay using static methods
        /// </summary>
        public  class RefundRequest
        {
            
            private string _WebServiceUrl = "https://securepgtest.fssnet.co.in/pgway/servlet/TranPortalXMLServlet";
            private string _PxPayUserId;
            private string _PxPayKey;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="PxPayUserId"></param>
            /// <param name="PxPayKey"></param>
            public RefundRequest(string PxPayUserId, string PxPayKey)
            {
                _PxPayUserId = PxPayUserId;
                _PxPayKey = PxPayKey;
            }

/// <summary>
/// 
/// </summary>
/// <param name="result"></param>
/// <returns></returns>
            public ResponseOutput ProcessResponse(string result)
            {

                ResponseOutput myResult = new ResponseOutput(SubmitXml(result));
                return myResult;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public RequestOutput GenerateRequest(string input)
            {
                //string xmlString1 = "<id>70003049</id><password>70003049</password><action>2</action><amt>300.00</amt><transid>14785226963212</transid><Member>Senthilkumar</Member><udf5>147852</udf5>";
                RequestOutput result = new RequestOutput(SubmitXml(input));
                return result;
            }

            private string SubmitXml(string InputXml)
            {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(_WebServiceUrl);
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(InputXml);
                //System.Xml.XmlNode xNode = doc.FirstChild;
                //doc.RemoveChild(xNode);

                webReq.Method = "POST";

                byte[] reqBytes;

                reqBytes = System.Text.Encoding.UTF8.GetBytes(InputXml);
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.ContentLength = reqBytes.Length;
                webReq.Timeout = 5000;
                Stream requestStream = webReq.GetRequestStream();
                requestStream.Write(reqBytes, 0, reqBytes.Length);
                requestStream.Close();

                HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.ASCII))
                {
                    return sr.ReadToEnd();
                }
            }

            /// <summary>
            /// Generates the XML required for a GenerateRequest call
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            private string GenerateRequestXml(RequestInput input)
            {

                StringWriter sw = new StringWriter();

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                //settings.NewLineHandling = NewLineHandling.Entitize;
                
                settings.NewLineOnAttributes = false;
                settings.OmitXmlDeclaration = true;
                //settings.ConformanceLevel = ConformanceLevel.Document;
                using (XmlWriter writer = XmlWriter.Create(sw, settings))
                {
                    writer.WriteStartDocument();

                    writer.WriteStartElement("GenerateRequest");

                    PropertyInfo[] properties = input.GetType().GetProperties();

                    foreach (PropertyInfo prop in properties)
                    {
                        if (prop.CanWrite)
                        {
                            string val = (string)prop.GetValue(input, null);

                            if (val != null || val != string.Empty)
                            {
                                
                                writer.WriteElementString(prop.Name, val);
                               
                            }
                        }
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                }
                return sw.ToString();
            }
            /// <summary>
            /// Generates the XML required for a ProcessResponse call
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            private string ProcessResponseXml(string result)
            {
                StringWriter sw = new StringWriter();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineOnAttributes = false;
                //settings.NewLineHandling=NewLineHandling.Entitize;
                settings.OmitXmlDeclaration = true;
                //settings.ConformanceLevel = ConformanceLevel.Document;
                using (XmlWriter writer = XmlWriter.Create(sw, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("ProcessResponse");
                    writer.WriteElementString("Response", result);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                }
                return sw.ToString();
            }

        }
        

        /// <summary>
        /// Class containing properties describing transaction details
        /// </summary>
        public class RequestInput
        {
            private string _Id;
            private string _Password;
            private string _Action;
            private string _Amount;
            private string _Udf5;
            private string _Member;
            private string _Transaction_Id;
            
            public RequestInput()
            {
            }
            public string id
            {
                get
                {
                    return _Id;
                }
                set
                {
                    _Id = value;
                }
            }

            public string password
            {
                get
                {
                    return _Password;
                }
                set
                {
                    _Password = value;
                }
            }

            public string action
            {
                get
                {
                    return _Action;
                }
                set
                {
                    _Action = value;
                }
            }
            public string amt
            {
                get
                {
                    return _Amount;
                }
                set
                {
                    _Amount = value;
                }
            }

            public string udf5
            {
                get
                {
                    return _Udf5;
                }
                set
                {
                    _Udf5 = value;
                }
            }

            public string Member
            {
                get
                {
                    return _Member;
                }
                set
                {
                    _Member = value;
                }
            }

            public string transid
            {
                get
                {
                    return _Transaction_Id;
                }
                set
                {
                    _Transaction_Id = value;
                }
            }
  // If there are any additional input parameters simply add a new read/write property
        }

        /// <summary>
        /// Class containing properties describing the output of the request
        /// </summary>
        public class RequestOutput
        {
            private string _Xml;
            public RequestOutput(string Xml)
            {
                _Xml = Xml;
                SetProperty();
            }
            private void SetProperty()
            {

                XmlReader reader = XmlReader.Create(new StringReader(_Xml));

                while (reader.Read())
                {
                    PropertyInfo prop;
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        prop = this.GetType().GetProperty(reader.Name);
                        if (prop != null)
                        {
                            this.GetType().GetProperty(reader.Name).SetValue(this, reader.ReadString(), System.Reflection.BindingFlags.Default, null, null, null);
                        }
                        if (reader.HasAttributes)
                        {

                            for (int count = 0; count < reader.AttributeCount; count++)
                            {
                                //Read the current attribute
                                reader.MoveToAttribute(count);
                                prop = this.GetType().GetProperty(reader.Name);
                                if (prop != null)
                                {
                                    this.GetType().GetProperty(reader.Name).SetValue(this, reader.Value, System.Reflection.BindingFlags.Default, null, null, null);
                                }
                            }
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Class containing properties describing the outcome of the transaction
        /// </summary>
        public class ResponseOutput
        {
            public ResponseOutput(string Xml)
            {
                _Xml = Xml;
                SetProperty();
            }
            private string _PayId;
            private string _Result;
            private string _Authendication;
            private string _Amount;
            private string _Reference;
            private string _PostDate;
            private string _Udf1;
            private string _Udf2;
            private string _Udf3;
            private string _Udf4;
            private string _Udf5;
            private string _Error_Service_Tag;
            private string _Error_Code_Tag;
            private string _Xml;

            public string payid
            {
                get
                {
                    return _PayId;
                }
                set
                {
                    _PayId = value;
                }
            }

            public string result
            {
                get
                {
                    return _Result;
                }
                set
                {
                    _Result = value;
                }
            }

            public string auth
            {
                get
                {
                    return _Authendication;
                }
                set
                {
                    _Authendication = value;
                }
            }

            public string amt
            {
                get
                {
                    return _Amount;
                }
                set
                {
                    _Amount = value;
                }
            }

            public string reff  
            {
                get
                {
                    return _Reference;
                }
                set
                {
                    _Reference = value;
                }
            }
            public string postdate
            {
                get
                {
                    return _PostDate;
                }
                set
                {
                    _PostDate = value;
                }
            }
            public string udf1
            {
                get
                {
                    return _Udf1;
                }
                set
                {
                    _Udf1 = value;
                }
            }

            public string udf2
            {
                get
                {
                    return _Udf2;
                }
                set
                {
                    _Udf2 = value;
                }
            }

            public string udf3
            {
                get
                {
                    return _Udf3;
                }
                set
                {
                    _Udf3 = value;
                }
            }

            public string udf4
            {
                get
                {
                    return _Udf4;
                }
                set
                {
                    _Udf4 = value;
                }
            }

            public string udf5
            {
                get
                {
                    return _Udf5;
                }
                set
                {
                    _Udf5 = value;
                }
            }

            public string error_service_tag
            {
                get
                {
                    return _Error_Service_Tag;
                }
                set
                {
                    _Error_Service_Tag = value;
                }
            }

            public string error_code_tag
            {
                get
                {
                    return _Error_Code_Tag;
                }
                set
                {
                    _Error_Code_Tag = value;
                }
            }
            // If there are any additional elements or attributes added to the output XML simply add a property of the same name.

            private void SetProperty()
            {

                XmlReader reader = XmlReader.Create(new StringReader(_Xml));

                while (reader.Read())
                {
                    PropertyInfo prop;
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        prop = this.GetType().GetProperty(reader.Name);
                        if (prop != null)
                        {
                            this.GetType().GetProperty(reader.Name).SetValue(this, reader.ReadString(), System.Reflection.BindingFlags.Default, null, null, null);
                        }
                        if (reader.HasAttributes)
                        {
                            for (int count = 0; count < reader.AttributeCount; count++)
                            {
                                //Read the current attribute
                                reader.MoveToAttribute(count);
                                prop = this.GetType().GetProperty(reader.Name);
                                if (prop != null)
                                {
                                    this.GetType().GetProperty(reader.Name).SetValue(this, reader.Value, System.Reflection.BindingFlags.Default, null, null, null);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
