using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System;
using System.Collections;

namespace ThePornDB.Helpers
{
    public class ActorStyle
    {
        public static (string Overview, string[] Tags) GetFormat(JObject actorData)
        {
            var cup = (string)actorData["extras"]["cupsize"];

            if (!string.IsNullOrEmpty(cup))
            {
                cup = Regex.Replace(cup, "[0-9]", string.Empty);
            }
            var placeholders = new Dictionary<string, string>()
            {
                { "{cup}", cup.ToUpper()},
                { "{po*}", "Pornstars" },
                { "{bio}", (string)actorData["bio"] },
                { "{hips}", (string)actorData["extras"]["hips"] },
                { "{waist}", (string)actorData["extras"]["waist"] },
                { "{active}", (string)actorData["extras"]["active"] },
                { "{height}", (string)actorData["extras"]["height"] },
                { "{weight}", (string)actorData["extras"]["weight"] },
                { "{gender}", (string)actorData["extras"]["gender"] },
                { "{cupsize}", (string)actorData["extras"]["cupsize"] },
                { "{tattoos}", (string)actorData["extras"]["tattoos"] },
                { "{birthday}", (string)actorData["extras"]["birthday"] },
                { "{piercings}", (string)actorData["extras"]["piercings"] },
                { "{ethnicity}", (string)actorData["extras"]["ethnicity"] },
                { "{astrology}", (string)actorData["extras"]["astrology"] },
                { "{birthplace}", (string)actorData["extras"]["birthplace"] },
                { "{hair_color}", (string)actorData["extras"]["hair_colour"] },
                { "{nationality}", (string)actorData["extras"]["nationality"] },
                { "{measurements}", (string)actorData["extras"]["measurements"] },
            };

            string overview = Plugin.Instance.Configuration.ActorsOverviewFormat;

            switch ((string)actorData["extras"]["gender"])
            {
                case "Female":
                    placeholders.Add("{tag}", "Girls");
                    overview = Regex.Replace(overview, @"<male>.*?</male>|<trans>.*?</trans>", string.Empty);
                    break;
                case "Trans":
                    placeholders.Add("{tag}", "Ladyboys");
                    overview = Regex.Replace(overview, @"<female>.*?</female>|<male>.*?</male>", string.Empty);
                    break;
                case "Male":
                    placeholders.Add("{tag}", "Boys");
                    overview = Regex.Replace(overview, @"<female>.*?</female>|<trans>.*?</trans>", string.Empty);
                    break;
                default:
                    placeholders.Add("{tag}", "Unk");
                    overview = Regex.Replace(overview, @"<female>.*?</female>|<trans>.*?</trans>|<male>.*?</male>", string.Empty);
                    break;
            }

            overview = Regex.Replace(overview, @"<.?male.|<.?female.|<.?trans.", string.Empty);
            overview = placeholders.Aggregate(overview, (current, parameter) => current.Replace(parameter.Key, parameter.Value));
            //overview = Regex.Replace(overview, @"{.*?}", string.emty);

            string taglist = Plugin.Instance.Configuration.ActorsTagList;
            taglist = placeholders.Aggregate(taglist, (current, parameter) => current.Replace(parameter.Key, parameter.Value));
            List <string> tags = new List <string>(taglist.Split(','));
           
            return (overview , tags.ToArray());
        }

    }
   
    public class CustomFormat
    {
        public enum ToFormat
        {
            Tagline = 1,
            Original = 2,
            Sortable = 3,
        }

        private static readonly string path_list = Path.Combine(Plugin.Instance.DataFolderPath, "data");

        public static string Get(JObject data,ToFormat field, bool useCustom ,string configFormat )
        {
            var format = string.Empty;
            var formatted = string.Empty;
            var actors = new JArray { };
            var no_male = new JArray { };

            foreach (var actor in data["performers"])
            {
                string gender = string.Empty;

                actors.Add((string)actor["name"]);

                if (actor["parent"] != null && actor["parent"].Type == JTokenType.Object)
                {
                    if (actor["parent"]["extras"]["gender"] != null)
                    {
                        gender = (string)actor["parent"]["extras"]["gender"];

                        if (gender == "Male" == false)
                        {
                            var dict_males = File.ReadLines(Path.Combine(path_list, "males.csv")).Select(line => line.Split(';')).ToDictionary(key => key[0], val => val[1]);
                            if (!dict_males.ContainsKey((string)actor["name"]))
                            {
                                no_male.Add((string)actor["name"]);
                            }

                        }

                    }
                }
            }

            string title = (string)data["title"],
                   studio = (string)data["site"]["name"],
                   id = (string)data["external_id"];

            if (useCustom)
                
            {
                var dictionary = File.ReadLines(Path.Combine(path_list, "sites.csv")).Select(line => line.Split(';')).ToDictionary(key => key[0], val => val[((int)field)]);

                if (dictionary.ContainsKey(studio))

                {
                    format = dictionary.FirstOrDefault(x => x.Key == studio).Value;
                }
                else
                { format = configFormat; }
                DateTime date = (DateTime)data["date"];

                var parameters = new Dictionary<string, object>()
              

                { { "{id}", id }, { "{title}", title }, { "{studio}", studio }, { "{release_date}", date.ToString("yyyy-MM-dd") },{"{actors}", string.Join (", ", actors) }, { "{no_male}",string.Join (", ", no_male)  } };
                formatted = parameters.Aggregate(format, (current, parameter) => current.Replace(parameter.Key, parameter.Value.ToString()));
                formatted = string.Join(" ", formatted.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                
            }
            return formatted;
        }
    }

       
}

