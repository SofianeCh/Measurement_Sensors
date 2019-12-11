using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Webreader2
{
/*	public class MyDateTimeConverter : IsoDateTimeConverter
	{
		public MyDateTimeConverter()
		{
			DateTimeFormat = "dd/MM/yyyy HH:mm:ss";
		}
	}*/
	class Param_sensors
	{
		//[JsonConverter(typeof(MyDateTimeConverter))]
		public DateTime start { get; set; }
		public float temp_C { get; set; }
		public float temp_F { get; set; }
		public float humidity { get; set; }
		public string moisture { get; set; }
	}
}
