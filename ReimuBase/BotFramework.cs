using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using ReimuAPI.ReimuBase;
using ReimuAPI.ReimuBase.Caller;
using ReimuAPI.ReimuBase.TgData;

namespace ReimuBotFramework.ReimuBase
{
    public class BotFramework
    {
        public BotFramework()
        {
            RAPI.loadPlugins();
        }

        internal void NewRequest(string JsonMessage)
        {
            TgBaseMessage data = null;
            try
            {
                data = (TgBaseMessage) new DataContractJsonSerializer(
                    typeof(TgBaseMessage)
                ).ReadObject(
                    new MemoryStream(
                        Encoding.UTF8.GetBytes(JsonMessage)
                    )
                );
            }
            catch (SerializationException e)
            {
                RAPI.GetExceptionListener().OnJsonDecodeError(e, JsonMessage);
                return;
            }

            CallPlugins(data, JsonMessage);
        }

        private void CallPlugins(TgBaseMessage message, string JsonMessage)
        {
            if (message.message != null) // 正常的消息
                new Thread(delegate()
                {
                    try
                    {
                        new NormalMessageCaller().call(message.message, JsonMessage);
                    }
                    catch (StopProcessException)
                    {
                    }
                    catch (Exception e)
                    {
                        RAPI.GetExceptionListener().OnException(e, JsonMessage);
                    }
                }).Start();
        }
    }
}