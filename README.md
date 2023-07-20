# ApexSpeed F1

Honours submission for BENG Software Engineering at Edinburgh Napier University.

## Overview
In our society, virtual e-sports are becoming increasingly popular, especially after the COVID-19 pandemic. Many major sporting events now have virtual versions, including and especially Formula 1 racing. As the skill level of drivers continues to rise, many of them have started using Telemetry applications to improve their performance much like drivers do for real on the track. However, these applications can be difficult for newcomers to understand and use effectively. This creates a gap that needs to be filled.

Researchers have studied how real-life Formula 1 telemetry is used to coach e-sports drivers, and they have found that the same concepts apply to the virtual counterparts. Some independent study has also been done which looked into the existing market for telemetry applications and found that there are no coaching tools specifically designed for newcomers.

Based on these findings, a prototype telemetry application was created. It includes both live and historical telemetry data to help determine whether real life Formula 1-based telemetry can assist new virtual players in improving their skills. Participants were invited to take part in a study to see if using the application could indeed make a difference.

The study revealed that 100% of the participants were able to enhance either their consistency or their lap times, and 62.5% of them were able to improve both aspects after using the telemetry data.
Further research showed that the participants had difficulties focusing on live telemetry data but had no problems analyzing historical telemetry. This indicates that more research should be conducted to understand why historical telemetry is more effective than live telemetry and whether this can be changed. 

## Application Preview

**Navigation to Live Telemetry Listener**

https://github.com/Suttie23/ApexSpeedF1/assets/76448183/35d02f81-a76e-44e9-9e4d-62752f7fafa6

Note that the live telemetry listener does not yet do anything, this is because there is no current F1 2021 session running.
<br></br>

**Live Telemetry Demonstration**

https://github.com/Suttie23/ApexSpeedF1/assets/76448183/06c5fc6d-d94a-445e-9869-89dff3d2f3bb

The UDP data is taken directly from the car and displayed onto the app. This view is useful for long stints, or over the course of a race, as it helps the driver to monitor their tyre wear, tyre temperature, fuel load and more.
<br></br>

**Historical Analysis Demonstration**

https://github.com/Suttie23/ApexSpeedF1/assets/76448183/16c18b66-61bb-4663-90c9-6af2bd0918b6

The application will record each datapoint transmitted from the car and will store it in a JSON file per lap. This allows the user to analyse their entire lap and compare them in an A/B comparison to determine why they may have been better or worse.
<br></br>

FULL DEMONSTRATION VIDEO: https://www.youtube.com/watch?v=oiiQ4QWR4-U&lc=Ugw9HKPf8v4A3w5sWKt4AaABAg

## Setup Guide

_Note, this is to be used when a build is avaliable. I will NOT be showing how to set up the application via an IDE manually_

Step 1: Download the ApexSpeed F1 build and run the application.

Step 2: Launch the F1 2021 game.

Step 3: Navigate to Options > Settings > Telemetry Settings and ensure that your page looks as seen below:

![image](https://github.com/Suttie23/ApexSpeedF1/assets/76448183/5b1e48bd-d027-4012-b2f7-2748d1abfb55)

Step 4: Navigate to the "Live Telemetry" page and click "Listen for UDP"

Step 5: Begin an F1 Session (Time trial, Grand Prix, etc) and you should see the live page updating as expected.

## Special Thanks

A big thank you to those at Napier for supporting me through this project.

A special thank you to [Tim Hanewich](https://github.com/TimHanewich) for his work on the [Codemasters / F1 2021 toolkit](https://github.com/TimHanewich/Codemasters.F1_2021). His work saved countless hours of document diving and coding in order to decode the UDP datastructures provided by codemasters and provided a brilliant and easy to use framework allowing me to complete this project.




