using Elasticsearch.Net;
using Nest;
using System;
using System.Net;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Globalization;

namespace Webreader2
{
	class Program
	{
		/// <summary>
		/// So We will stream data from a raspberry pi4 to our computer and put everything on our ES.
		/// All data has been put on a local server and we are going to read the page and extract the data from there.
		/// Webpage return NULL = there has been an issues with the raspberry pi
		/// webpage should return "TIME, temp in C, temp in F and humidity"
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Uri node = new Uri("http://localhost:9200");
			SingleNodeConnectionPool pool = new SingleNodeConnectionPool(node);
			var config = new ConnectionConfiguration(pool);
			var settings = new ConnectionSettings(node)
				.DefaultIndex("sensors");
			var clientP = new ElasticClient(new ConnectionSettings(pool));
			ElasticClient clientS = new ElasticClient(settings);

			using (WebClient client = new WebClient())
			{
				while (true)
				{
					var url = "http://192.168.1.206:8080";
					string s = client.DownloadString(url);
					Console.WriteLine(s);

					var array = s.Split(',');
					var sensor_p = new Param_sensors();
					int index = 0;
					bool problem = false;
					foreach (var item in array)
					{
						var parse = item;
						Console.WriteLine("new boucle  " + parse);
						if (index == 0)
						{
							parse.Replace("\u200B", "");
							var check = DateTime.Parse(parse);
							sensor_p.start = check;
							Console.WriteLine("index     " + index + "  " + check);
						}
						else if (index == 1)
						{
							sensor_p.humidity = float.Parse(parse, CultureInfo.InvariantCulture);
							Console.WriteLine(sensor_p.humidity);
							if (sensor_p.humidity == 0)
							{
								problem = true;
								Console.WriteLine("humidity issue " + index + "  " + sensor_p.humidity);
								break;
							}
						}
						else if (index == 2)
						{
							sensor_p.temp_C = float.Parse(parse, CultureInfo.InvariantCulture);
							if (sensor_p.temp_C == 0)
							{
								problem = true;
								Console.WriteLine("temp c issue");
								break;
							}
						}
						else if (index == 3)
						{
							sensor_p.temp_F = float.Parse(parse, CultureInfo.InvariantCulture);
							if (sensor_p.temp_F == 0)
							{
								problem = true;
								Console.WriteLine("temp F issue");
								break;
							}
						}
						else
						{
							sensor_p.moisture = item;
						}
						index++;
					}
					if (!problem)
						clientS.IndexDocument(sensor_p);
					else
						Console.WriteLine("issue");
					System.Threading.Thread.Sleep(10000);
				}
			}
		}
	}
}
