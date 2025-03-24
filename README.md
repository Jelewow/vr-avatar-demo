# **VR Avatar for Unity 2018+**
![Static Badge](https://img.shields.io/badge/Unity-C%23-purple?labelColor=black)

> [!IMPORTANT]  
> This works only with Humanoid Rig

## **Result**
Click to open in YouTube

[![jelewow](https://img.youtube.com/vi/mGLk2uvsFUY/0.jpg)](https://www.youtube.com/watch?v=mGLk2uvsFUY "Vr Avatar Demo")

## **About**
This demo project implements a VR avatar using the built-in Unity IK system, without using any third-party solutions.
In this implementation there is no binding to a specific VR device and should work on any 6-dof devices with minimal configuration of parameters from the Unity inspector

## **Demo project information**
Setup of the project is done with the help of VR Core template with Unity 6000.0.33f1. The project mechanics were tested on Pico 4 device using <a href="https://developer.picoxr.com/document/unity-openxr/">**PICO Unity OpenXR SDK**</a> (version 1.3.0)

The character model and animations were taken from <a href="https://www.mixamo.com/#/">**mixamo**</a>

## **Tips for improving animations**
If you have something wrong with your arms, legs, or other body parts, you should configure avatar in unity. My hand configuration in this project (left and right are identical)

![image](https://github.com/user-attachments/assets/50a57716-fbf0-4eda-9053-c10087cef4db)

as well as stretching of body parts

![image](https://github.com/user-attachments/assets/f84e5aeb-cac8-4064-8928-d43c0366bb7b)

It's also worth creating a humanoid avatar mask or customizing a mask for each animation.

![image](https://github.com/user-attachments/assets/b95d1bbb-b990-41c3-bfbb-79852032e0f1)

## **Additional**
The project also implements ik finger animation for any controller. It is not present in the demo project itself, as it is necessary to have hand animation for a specific controller for this purpose
