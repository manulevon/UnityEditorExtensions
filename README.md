# UnityEditorExtensions
This project will include the extension for Editor Unity3D.  It will be constantly updated with useful plugins.
## Auto Set texture in material
This extension is designed to automatically set textures in the material. Used map only Albedo, Normal, Spekular, Metalli
### Getting Started
Download the script "AutoSetextureInMaterial" and put it in the folder Editor. 
### How it works
Click on FBX with the right mouse button, then select SetTextureObject. 
If in the folder in which the selected object is placed materials with textures or folders with materials and textures, the script will find each material, then find the textures, and set them into the material if they are named "MaterialName_NameMaps"(Planet_Albedo, Planet_Normal).
