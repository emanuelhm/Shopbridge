# Shopbridge

This is an API used to CRUD basic informations about Products from Shopbridge.

Unlike the base project, this application was build using the .net 6 LTS.

## Some other implementations where also performed like:
* It's now possible to create tags and categories and link them to a specific product;
* When creating/updating a product, a image upload option is available;
* All exceptions are handled automaticaly through ErrorController, the error is logged but nothing is returned to the end user for security reasons;

## Some libraries used:
* Automapper was used to easily transfer data throut the entity to Dtos;
* Cloudinary is a CDN that also is capable of storing and easily manipulating images. It was used because of its high limitations even in free tier, high security, possiblity of realtime image manipualtion and ease of use.

## **IMPORTANT!** 
The ".env" file and connection string where omited for security reasons!
