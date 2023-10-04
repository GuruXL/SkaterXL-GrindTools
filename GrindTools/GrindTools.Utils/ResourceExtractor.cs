using System.Reflection;
using UnityEngine;
using System.IO;
using System.Collections;
using System;
using ModIO.UI;

namespace GrindTools.Utils
{
    public static class ResourceExtractor
    {
        public static byte[] ExtractResources(string filename)
        {
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
            {
                if (manifestResourceStream == null)
                    return null;

                byte[] buffer = new byte[manifestResourceStream.Length];
                manifestResourceStream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
    }
}
