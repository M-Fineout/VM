//NOTE: to work on both Http and Https -- omit the http: for all requests and leave just the //
//this will allow for both unsecure, as well as secure servers, but won't allow for loading on localhost!

/*jslint browser:true */
'use strict';

var weatherConditions = new XMLHttpRequest();
var weatherForecast = new XMLHttpRequest();
var cObj;
var fObj;

// GET THE CONDITIONS
weatherConditions.open('GET', "http://api.openweathermap.org/data/2.5/weather?zip=32514,us&APPID=8ce3c686a98b883f0c026068c75371e5&units=imperial", true);
weatherConditions.responseType = 'text';
weatherConditions.send(null);

weatherConditions.onload = function() {
    if (weatherConditions.status === 200){
        cObj = JSON.parse(weatherConditions.responseText);
        console.log(cObj);
        document.getElementById('location').innerHTML=cObj.name;
        document.getElementById('weather').innerHTML=cObj.weather[0].main;
        document.getElementById('temperature').innerHTML=cObj.main.temp;
        document.getElementById('desc').innerHTML=cObj.weather[0].description;


    } //end if
}; //end function


// GET THE FORECAST
weatherForecast.open('GET', "http://api.openweathermap.org/data/2.5/forecast?zip=32514,us&APPID=8ce3c686a98b883f0c026068c75371e5&units=imperial", true);
weatherForecast.responseType = 'text';
weatherForecast.send();

weatherForecast.onload = function() {
if (weatherForecast.status === 200){
	fObj = JSON.parse(weatherForecast.responseText);
	console.log(fObj);

  //format date
  var date_raw = fObj.list[0].dt_txt;
  date_raw = date_raw.substring(5,11);
  //display date
  document.getElementById('r1c1').innerHTML=date_raw;

  //parse icon and format path
  var iconCode = fObj.list[0].weather[0].icon;
  var iconPath = "http://openweathermap.org/img/w/" + iconCode + ".png";
  //set icon
  document.getElementById('r1c2').src=iconPath;

  document.getElementById('r1c3').innerHTML="Min temp " + fObj.list[0].main.temp_min + "&deg;";
  document.getElementById('r1c4').innerHTML="Max temp " + fObj.list[0].main.temp_max + "&deg";

  //--------------------------------
  //format date
  var date_raw = fObj.list[8].dt_txt;
  date_raw = date_raw.substring(5,11);
  //display date
  document.getElementById('r2c1').innerHTML=date_raw;

  //parse icon and format path
  var iconCode = fObj.list[8].weather[0].icon;
  var iconPath = "http://openweathermap.org/img/w/" + iconCode + ".png";
  //set icon
  document.getElementById('r2c2').src=iconPath;

  document.getElementById('r2c3').innerHTML="Min temp " + fObj.list[8].main.temp_min + "&deg;";
  document.getElementById('r2c4').innerHTML="Max temp " + fObj.list[8].main.temp_max + "&deg";

  //----------------------------------
  //format date
  var date_raw = fObj.list[16].dt_txt;
  date_raw = date_raw.substring(5,11);
  //display date
  document.getElementById('r3c1').innerHTML=date_raw;

  //parse icon and format path
  var iconCode = fObj.list[16].weather[0].icon;
  var iconPath = "http://openweathermap.org/img/w/" + iconCode + ".png";
  //set icon
  document.getElementById('r3c2').src=iconPath;

  document.getElementById('r3c3').innerHTML="Min temp " + fObj.list[0].main.temp_min + "&deg;";
  document.getElementById('r3c4').innerHTML="Max temp " + fObj.list[0].main.temp_max + "&deg";



} //end if
}; //end function
