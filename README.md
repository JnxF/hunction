# hunction

Submission for Junction 2018 AI/Big Data track: 

**CISCO: CAPTURE THE CREATURE**
*Augmented reality creature hunting game to entertain young super market customers.*

Today’s retail companies look for new ways to drive customer loyalty and increase number of visits to their physical stores. In this challenge, you will develop an indoor, location-aware creature hunting game for smartphones, based on the BLE beacon or Wi-Fi location technologies in the Cisco Meraki solution. The aim of the game is to entertain young visitors and get their parents to visit store sections with offers that might interest them.

<img src="imatges/hunction.png">

### About

We aimed to create a game for children to play while their parents are shopping. Not only would it be an opportunity for them to play together, but also encourage its use by offering sales or discounts based on one's ability in the game and thus strengthening the loyalty of customers. 

We used Unity for the Application.  The app offers an Augmented Reality experience in which you can visualize and fight against monsters. When a monster is defeated some points are added to the users' score. The score can be exchanged for discounts or other advantages. 

The backend is comprised of Flask and SQLAlchemy. The API is in charge of handling the interaction between Cisco's API and the application. The former provides the user's location inside the supermarket using the WiFi beacons. Once the location data is in Unity, we project the real-life coordinates to the augmented reality world. 

Unfortunately, the refresh rate was not fast enough for it to be used to track the movement of the player in the game in real time, therefore, we came up with a slightly different approach.

Instead of tracking the users' location in real time, we wait until the Cisco's API notifies us when the player is less than 2 meters away from the monster.

The last component of this project, the web, allows supermarket managers to choose the monster's location. This feature allows placing them at locations which suit the manager's interests such as attracting costumers to products.  

### Authors

* [JnxF](https://github.com/JnxF)
* [marcbenedi](https://github.com/marcbenedi)
* [david-campos](https://github.com/david-campos)
* [mbdavid2](https://github.com/mbdavid2)

### Used technologies

* Unity (C#)
* Flask (SQLAlchemy)
* Cisco Meraki (Scan API)

### Examples

[Youtube Playlist](https://www.youtube.com/playlist?list=PLQzDx9j9ch_J-bQLoZ6g0KFGLq-_zPBJy)

### Images

<img src="imatges/junc1.jpg" width="200px"><img src="imatges/junc2.jpg" width="200px">
<img src="imatges/junc3.jpg" width="200px"><img src="imatges/junc4.jpg" width="200px">
<img src="imatges/junc 5.jpg" width="200px">

### Credit

* [Amusedart (Monster model and animations)](https://assetstore.unity.com/packages/3d/characters/creatures/fantasy-mushroom-mon-115406)