## TextureGradation  

Generate gradatuion texture asset as a custom asset importer.  

### List of Gradation Shapes
- Line  
![pic](https://i.imgur.com/FLHtDR3.png)


- Circular  
![pic](https://i.imgur.com/DYHRMXm.png)


- Radial  
![pic](https://i.imgur.com/hFWLsUX.png)


- Diagonal  
![pic](https://i.imgur.com/Jrf535M.png)


- Box  
![pic](https://i.imgur.com/e8SoqyB.png)


## Install
It can be installed by adding scoped registry to the manifest file(Packages/manifest.json).

`scopedRegistries`
````
{
    "name": "torinos",
    "url": "https://registry.npmjs.com",
    "scopes": ["jp.torinos"]
}
````
`dependencies`
````
"jp.torinos.texturegradation": "1.0.0"
````