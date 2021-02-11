using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using SFML.Window;

namespace EngineSFML.Main
{
    public sealed class Settings
    {

        private static readonly Lazy<Settings> instance = 
            new Lazy<Settings>(() => new Settings());
        public static Settings Instance { get { return instance.Value; } }

        private const string WindowTitle = "TEST 1.0v";

        private XmlDocument xmlDocumentWindow;
        private XmlDocument xmlDocumentControl;

        private Dictionary<string, string> window;
        private Dictionary<string, string> control;

        private Settings()
        {
            xmlDocumentWindow = new XmlDocument();
            xmlDocumentControl = new XmlDocument();

            window = new Dictionary<string, string>();
            control = new Dictionary<string, string>();

            window.Add("title", WindowTitle);

            if (!Directory.Exists("Resources"))
                Directory.CreateDirectory("Resources");

            if (!Directory.Exists("Resources\\Config"))
                Directory.CreateDirectory("Resources\\Config");


            try
            {
                xmlDocumentWindow.Load("Resources\\Config\\Window.xml");
            } catch {
                CreateDefaultWindowConfig();
                xmlDocumentWindow.Load("Resources\\Config\\Window.xml");
            }

            try
            {
                xmlDocumentControl.Load("Resources\\Config\\Control.xml");
            }
            catch
            {
                CreateDefaultControlConfig();
                xmlDocumentControl.Load("Resources\\Config\\Control.xml");
            }

            LoadConfigs();
        }

        private void LoadConfigs()
        {
            XmlNode rootWindow = xmlDocumentWindow.DocumentElement;

            foreach (XmlNode node in rootWindow.ChildNodes)
            {
                window.Add(node.Name, node.InnerText);
            }

            XmlNode rootControl = xmlDocumentControl.DocumentElement;

            foreach (XmlNode node in rootControl.ChildNodes)
            {
                control.Add(node.Name, node.InnerText);
            }
        }

        private void CreateDefaultWindowConfig()
        {
            if (File.Exists("Resources\\Config\\Window.xml"))
                File.Delete("Resources\\Config\\Window.xml");

            xmlDocumentWindow.AppendChild(xmlDocumentWindow.CreateXmlDeclaration("1.0", "UTF-8", null));

            XmlNode root = xmlDocumentWindow.CreateElement("configs");

            root.AppendChild(CreateConfigNode(xmlDocumentWindow, "width", "640"));
            root.AppendChild(CreateConfigNode(xmlDocumentWindow, "height", "480"));

            root.AppendChild(CreateConfigNode(xmlDocumentWindow, "framelimit", "60"));
            root.AppendChild(CreateConfigNode(xmlDocumentWindow, "vertsync", "true"));

            xmlDocumentWindow.AppendChild(root);
            xmlDocumentWindow.Save("Resources\\Config\\Window.xml");
        }

        private void CreateDefaultControlConfig()
        {
            if (File.Exists("Resources\\Config\\Control.xml"))
                File.Delete("Resources\\Config\\Control.xml");

            xmlDocumentControl.AppendChild(xmlDocumentControl.CreateXmlDeclaration("1.0", "UTF-8", null));

            XmlNode root = xmlDocumentControl.CreateElement("configs");

            root.AppendChild(CreateConfigNode(xmlDocumentControl, "up", ((int)Keyboard.Key.Up).ToString()));
            root.AppendChild(CreateConfigNode(xmlDocumentControl, "down", ((int)Keyboard.Key.Down).ToString()));
            root.AppendChild(CreateConfigNode(xmlDocumentControl, "left", ((int)Keyboard.Key.Left).ToString()));
            root.AppendChild(CreateConfigNode(xmlDocumentControl, "right", ((int)Keyboard.Key.Right).ToString()));
            root.AppendChild(CreateConfigNode(xmlDocumentControl, "close", ((int)Keyboard.Key.Escape).ToString()));

            xmlDocumentControl.AppendChild(root);
            xmlDocumentControl.Save("Resources\\Config\\Control.xml");
        }

        private XmlNode CreateConfigNode(XmlDocument xml, string _name, string _value)
        {
            XmlNode node = xml.CreateElement(_name);
            node.InnerText = _value;
            return node;
        }

        public string GetConfig(string _name)
        {
            string[] splited = _name.Split(".");
            if (splited.Length >= 2)
            {
                string type = splited[0];
                string config = splited[1];

                switch (type)
                {
                    case "window":
                        if (window.ContainsKey(config))
                            return window[config];
                        else
                            return "not found";
                    case "control":
                        if (control.ContainsKey(config))
                            return control[config];
                        else
                            return "not found";
                    default:
                        return "not found";
                }
            }
            return "not found";
        }

    }
}
