# AirportsDistance
REST service to measure distance in miles between two airports. Airports are identified by 3-letter IATA code. 

The simplest method of calculating distance relies on some advanced-looking math.  Known as the Haversine formula, it uses spherical trigonometry to determine the great circle distance between two points. [Wikipedia](https://en.wikipedia.org/wiki/Haversine_formula)  has more on the formulation of this popular straight line distance approximation.

For example:
* SVO for Moscow Sheremetyevo
* EWR for Newark, New Jersey
* HVN for New Haven, Connecticut
* ORF for Norfolk, Virginia
* EYW for Key West, Florida
* OME for Nome, Alaska
* BNA for Nashville, Tennessee (whose airport's original name was Berry Field)
* APC for Napa, California
* ILM for Wilmington, North Carolina

# Launch
Depedency: .Net8, node.js 20v+

``` 
npm install
```
Follow: [https://localhost:7032](https://localhost:5173/)

For swagger: https://localhost:7032/swagger/index.html
