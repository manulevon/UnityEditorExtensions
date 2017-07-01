# UnityEditorExtensions
This project will include the extension for Editor Unity3D.  It will be constantly updated with useful plugins.
## Auto Set texture in material
This extension is designed to automatically set textures in the material. Used map only Albedo, Normal, Spekular, Metalli
### Getting Started
Download the script "AutoSetextureInMaterial" and put it in the folder Editor. 
### How it works
Click on FBX with the right mouse button, then select SetTextureObject. 
If in the folder in which the selected object is placed materials with textures or folders with materials and textures, the script will find each material, then find the textures, and set them into the material if they are named "MaterialName_NameMaps"(Planet_Albedo, Planet_Normal).

![imageone](https://user-images.githubusercontent.com/19221189/27763324-5f4e1c32-5e8a-11e7-848b-e7528eef5128.png)

![imagetwo](https://user-images.githubusercontent.com/19221189/27763335-82290776-5e8a-11e7-8173-f5dced8ccdd6.png)

If you right-click an empty area in the Project tab, the SetTextureObject button will not be active. But the SetTextureProject button will be active, this function will find all the folders in which there is FBX, and if there are materials with textures, assign the map to the materials.

![imagethree](https://user-images.githubusercontent.com/19221189/27763337-8b518ac6-5e8a-11e7-997f-2ef223721f67.png)

