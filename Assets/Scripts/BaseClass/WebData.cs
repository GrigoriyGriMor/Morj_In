using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebData
{
    private static string headerName = "HTTP_X_REQUESTED_WITH";
    public static string HeaderName => headerName;
    private static string headerValue = "XMLHttpRequest";
    public static string HeaderValue => headerValue;

    private static string context_type = "HTTP_ACCEPT";
    public static string ContexTypeName => context_type;
    private static string context_type_value = "application/json";
    public static string ContexTypeValue => context_type_value;
}