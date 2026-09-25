using System.Collections.Generic;
using System.Text;

namespace JTLStudio.SDK.Analytics
{
    public static class AnalyticsJson
    {
        public static string Write(Dictionary<string, string> data)
        {
            if (data == null || data.Count == 0)
            {
                return "{}";
            }

            StringBuilder builder = new StringBuilder();
            builder.Append('{');
            bool first = true;

            foreach (KeyValuePair<string, string> pair in data)
            {
                if (first == false)
                {
                    builder.Append(',');
                }

                first = false;
                Append(builder, pair.Key);
                builder.Append(':');
                Append(builder, pair.Value);
            }

            builder.Append('}');
            return builder.ToString();
        }

        private static void Append(StringBuilder builder, string value)
        {
            builder.Append('"');

            if (value != null)
            {
                foreach (char symbol in value)
                {
                    switch (symbol)
                    {
                        case '"':
                            builder.Append("\\\"");
                            break;

                        case '\\':
                            builder.Append("\\\\");
                            break;

                        case '\n':
                            builder.Append("\\n");
                            break;

                        case '\r':
                            builder.Append("\\r");
                            break;

                        case '\t':
                            builder.Append("\\t");
                            break;

                        default:
                            builder.Append(symbol);
                            break;
                    }
                }
            }

            builder.Append('"');
        }
    }
}
