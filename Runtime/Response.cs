using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Creator.Utilities
{
    public enum Response
    {
        Success,
        ApiRequestSuccess,
        Fail,
        UIFail
    }
    
    public static class ResponseExtensions
    {
        public static Response Debug(this Response response)
        {
            D.Info("Response - ", response.ToString());
            return response;
        }
    }
}
