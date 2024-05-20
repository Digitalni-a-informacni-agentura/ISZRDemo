// -----------------------------------------------------------------------
// <copyright file="MessageInspector.cs" company="AucoContCZ">
// Zdrojový kód aplikace i přeloženou aplikaci lze použít libovolným způsobem. 
// Tvůrce aplikace a ostatních částí (aplikace, zdrojové kódy, dokumentace, 
// certifikát, ostatní soubory a další) není v žádném ohledu odpovědný za jakýkoliv 
// důsledek přímo nebo nepřímo vzniklý v souvislosti s libovolnou částí díla.
// </copyright>
// -----------------------------------------------------------------------

namespace Autocont.ISZRDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;
    using System.Xml;
    using System.IO;

    /// <summary>
    /// Trida pro logovani pozadavku a odpovedi webove sluzby
    /// </summary>
    public class MessageInspector : IClientMessageInspector
    {
        #region privatni promenne
        private ILog log;
        #endregion
        //------------------------------------------------------------------------------------
        public MessageInspector(ILog log)
        {
            this.log = log;
        }
        //------------------------------------------------------------------------------------
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            String msg = reply.ToString();
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(msg);
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                XmlTextWriter xtw = null;
                try
                {
                    xtw = new XmlTextWriter(sw);
                    xtw.Formatting = Formatting.Indented;
                    xd.WriteTo(xtw);
                }
                finally
                {
                    if (xtw != null) xtw.Close();
                }
                msg = sb.ToString();
            }
            catch (Exception)
            {
            }
            log.Log(Environment.NewLine + msg + Environment.NewLine);
        }
        //------------------------------------------------------------------------------------
        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            log.Log(Environment.NewLine + request.ToString() + Environment.NewLine);
            return null;
        }
    }
    /// <summary>
    /// Trida pro logovani pozadavku a odpovedi webove sluzby
    /// </summary>
    public class MessageLoggerBehavior : IEndpointBehavior
    {
        private ILog log;
        //------------------------------------------------------------------------------------
        public MessageLoggerBehavior(ILog log)
        {
            this.log = log;
        }
        //------------------------------------------------------------------------------------
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }
        //------------------------------------------------------------------------------------
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new MessageInspector(log));
        }
        //------------------------------------------------------------------------------------
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            throw new NotImplementedException();
        }
        //------------------------------------------------------------------------------------
        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
