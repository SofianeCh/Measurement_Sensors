"# Measurement_Sensors" 

With this small project, I extract data from an RPI4 with a small python code.
I'll use the DHT11 to extract the temp and humidity and a moisture sensor to check whether a plant we'll need water.
Afterward, I'll connect everything with a local server where the data will be accessible in a CSV format.

On my computer, I'll launch a c# code that will access the server, extract the data and put everything into elasticsearch.

Once everything is set up and you have a nice DB you can visualize the data with kibana which will provide a nice overview. 
