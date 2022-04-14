using System.Xml;
using System.IO;
using System;
using UnityEngine;

namespace StudyGame
{
    public sealed class DataController : Controller
    {
        string saveDirectory = Path.Combine(Application.dataPath, "Saves");
        string savePath = Path.Combine(Application.dataPath, @"Saves\Save.xml");

        public delegate void NoSaves();
        public event NoSaves CreateAutoSave;
        public override void StartController()
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }
        }

        public void Save (LevelInfo[] levels, PlayerInfo player, int activeLevel)
        {
            SaveStructure save = new SaveStructure(levels, player, activeLevel);
            XmlDocument file = new XmlDocument();
            XmlNode savedData = file.CreateElement("Data");
            file.AppendChild(savedData);

            XmlElement element = file.CreateElement("XPos");
            element.SetAttribute("value", save.XPos.ToString());
            savedData.AppendChild(element);
            element = file.CreateElement("YPos");
            element.SetAttribute("value", save.YPos.ToString());
            savedData.AppendChild(element);
            element = file.CreateElement("ZPos");
            element.SetAttribute("value", save.ZPos.ToString());
            savedData.AppendChild(element);

            element = file.CreateElement("XRot");
            element.SetAttribute("value", save.XRot.ToString());
            savedData.AppendChild(element);
            element = file.CreateElement("YRot");
            element.SetAttribute("value", save.YRot.ToString());
            savedData.AppendChild(element);
            element = file.CreateElement("ZRot");
            element.SetAttribute("value", save.ZRot.ToString());
            savedData.AppendChild(element);
            element = file.CreateElement("WRot");
            element.SetAttribute("value", save.WRot.ToString());
            savedData.AppendChild(element);

            element = file.CreateElement("Energy");
            element.SetAttribute("value", save.Energy.ToString());
            savedData.AppendChild(element);
            element = file.CreateElement("Health");
            element.SetAttribute("value", save.Health.ToString());
            savedData.AppendChild(element);
            element = file.CreateElement("MovementSpeed");
            element.SetAttribute("value", save.MovementSpeed.ToString());
            savedData.AppendChild(element);
            element = file.CreateElement("JumpSpeed");
            element.SetAttribute("value", save.JumpSpeed.ToString());
            savedData.AppendChild(element);

            string tempName = "LevelVariants";
            element = file.CreateElement(tempName);
            savedData.AppendChild(element);
            for (int i = 0; i < save.LevelVariants.Length; i++)
            {
                XmlElement intValue = file.CreateElement("IntValue");
                intValue.SetAttribute("value", save.LevelVariants[i].ToString());
                element.AppendChild(intValue);
            }

            tempName = "Activated";
            element = file.CreateElement(tempName);
            savedData.AppendChild(element);
            for (int i = 0; i < save.Activated.Length; i++)
            {
                XmlElement boolValue = file.CreateElement("BoolValue");
                boolValue.SetAttribute("value", save.Activated[i].ToString());
                element.AppendChild(boolValue);
            }

            element = file.CreateElement("ActiveLevel");
            element.SetAttribute("value", activeLevel.ToString());
            savedData.AppendChild(element);

            file.Save(savePath);
        }

        public SaveStructure Load()
        {
            SaveStructure loaded = new SaveStructure();

            if (!File.Exists(savePath))
                CreateAutoSave.Invoke();
            XmlDocument save = new XmlDocument();
            save.Load(savePath);
            XmlElement root = save.DocumentElement;
            if (root != null)
            {
                foreach (XmlElement element in root)
                {
                    switch (element.Name)
                    {
                        case "XPos":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
                                    loaded.XPos = float.Parse(attr.Value);
                                }
                                break;
                            }                        
                        case "YPos":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
                                    loaded.YPos = float.Parse(attr.Value);
                                }
                                break;
                            }                        
                        case "ZPos":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
                                    loaded.ZPos = float.Parse(attr.Value);
                                }
                                break;
                            }                        
                        case "XRot":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
                                    loaded.XRot = float.Parse(attr.Value);
                                }
                                break;
                            }                        
                        case "YRot":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
                                    loaded.YRot = float.Parse(attr.Value);
                                }
                                break;
                            }                        
                        case "ZRot":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
                                    loaded.ZRot = float.Parse(attr.Value);
                                }
                                break;
                            }                        
                        case "WRot":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
                                    loaded.WRot = float.Parse(attr.Value);
                                }
                                break;
                            }                        
                        case "Energy":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
                                    loaded.Energy = float.Parse(attr.Value);
                                }
                                break;
                            }                        
                        case "Health":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
  
                                    loaded.Health = float.Parse(attr.Value);
                                }
                                break;
                            }                        
                        case "MovementSpeed":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
                                    loaded.MovementSpeed = float.Parse(attr.Value);
                                }
                                break;
                            }                        
                        case "JumpSpeed":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
       
                                    loaded.JumpSpeed = float.Parse(attr.Value);
                                }
                                break;
                            }
                        case "LevelVariants":
                            {
                                int range = 0;
                                foreach (XmlElement childElement in element.ChildNodes)
                                {
                                    range++;
                                }
                                loaded.LevelVariants = new int[range];
                                range = 0;
                                foreach (XmlElement childElement in element.ChildNodes)
                                {
                                    foreach (XmlAttribute attr in childElement.Attributes)
                                    {
                                        loaded.LevelVariants[range] = int.Parse(attr.Value);
                                    }
                                    range++;
                                }
                                break;
                            }
                        case "Activated":
                            {
                                int range = 0;
                                foreach (XmlElement childElement in element.ChildNodes)
                                {
                                    range++;
                                }
                                loaded.Activated = new bool[range];
                                range = 0;
                                foreach (XmlElement childElement in element.ChildNodes)
                                {
                                    foreach (XmlAttribute attr in childElement.Attributes)
                                    {
                                        loaded.Activated[range] = Convert.ToBoolean(attr.Value);
                                    }
                                    range++;
                                }
                                break;
                            }
                        case "ActiveLevel":
                            {
                                foreach (XmlAttribute attr in element.Attributes)
                                {
                                    loaded.ActiveLevel = int.Parse(attr.Value);
                                }
                                break;
                            }
                            
                    }
                }
            }
            return loaded;
        } 
    }
}

