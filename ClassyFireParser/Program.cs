using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ClassyFireParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                InvalidInput();
                System.Environment.Exit(1);
            }
            StringBuilder stringbuilder = new StringBuilder();
            try
            {
                StreamReader FileReader = new StreamReader(args[0]);
                string jsonFile = FileReader.ReadToEnd();
                JsonTextReader jsonReader = new JsonTextReader(new StringReader(jsonFile));

                dynamic json = JsonConvert.DeserializeObject(jsonFile);

                var entities = json.entities;

                string header = "identifier\tsmiles\tinchikey\tkingdomName\tkingdomID\tsuperclassName\tsuperclassID\tclassName\tclassID\tsubclassName\tsubclassID\tdirectparentName\tdirectparentID";
                stringbuilder.AppendLine(header);

                foreach (var entity in entities)
                {
                    string identifier = "";
                    string smiles = "";
                    string inchikey = "";
                    string kingdomName = "";
                    string kingdomID = "";
                    string superclassName = "";
                    string superclassID = "";
                    string className = "";
                    string classID = "";
                    string subclassName = "";
                    string subclassID = "";
                    string directparentName = "";
                    string directparentID = "";

                    if (entity.identifier != null) identifier = entity.identifier;
                    if (entity.smiles != null) smiles = entity.smiles;
                    if (entity.inchikey != null) inchikey = entity.inchikey;
                    if (entity.kingdom != null) kingdomName = entity.kingdom.name;
                    if (entity.kingdom != null) kingdomID = entity.kingdom.chemont_id;
                    if (entity.superclass != null) superclassName = entity.superclass.name;
                    if (entity.superclass != null) superclassID = entity.superclass.chemont_id;
                    if (entity.@class != null) className = entity.@class.name;
                    if (entity.@class != null) classID = entity.@class.chemont_id;
                    if (entity.subclass != null) subclassName = entity.subclass.name;
                    if (entity.subclass != null) subclassID = entity.subclass.chemont_id;
                    if (entity.direct_parent != null) directparentName = entity.direct_parent.name;
                    if (entity.direct_parent != null) directparentID = entity.direct_parent.chemont_id;


                    var line = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}",
                        identifier, smiles, inchikey, kingdomName, kingdomID, superclassName, superclassID, className, classID,
                        subclassName, subclassID, directparentName, directparentID);
                    stringbuilder.AppendLine(line);
                }

                var directory = Directory.GetParent(args[0]);

                using (StreamWriter writer = new StreamWriter(String.Format("{0}_ChemOnt.tsv", args[0].Replace(".json", ""))))
                {
                    writer.Write(stringbuilder);
                }
            }
            catch(Exception ex)
            {
                InvalidInput();
            }

        }
        
        static void InvalidInput()
        {
            Console.WriteLine("Invalid input");
            Console.WriteLine("Example call: ClassyFireParser.exe example.json");
        }

    }
}
