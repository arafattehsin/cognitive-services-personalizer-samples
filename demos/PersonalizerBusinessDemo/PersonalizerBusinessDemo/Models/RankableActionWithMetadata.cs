﻿using Microsoft.Azure.CognitiveServices.Personalizer.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace PersonalizerBusinessDemo.Models
{
    public class RankableActionWithMetadata : RankableAction
    {
        public RankableActionWithMetadata(Article article)
        {
            Id = article.Id;
            ImageName = article.ImageName;
            Title = article.Title;
            Features = new List<object>()
            {
                    new {article.Source},
                    new {article.HasVideo},
                    new {article.PublishedAgo}
            };

            if (article.EditorialHighlight.HasValue)
            {
                Features.Add(new { article.EditorialHighlight });
            }

            if (article.Entities != null && article.Entities.Any())
            {
                ExpandoObject entities = new ExpandoObject();

                foreach (string key in article.Entities.Keys)
                {
                    entities.TryAdd(key, article.Entities[key]);

                }

                Features.Add(new { entities });
            }

            if (article.Tags != null && article.Tags.Any())
            {
                ExpandoObject tags = new ExpandoObject();

                foreach (string tag in article.Tags)
                {
                    tags.TryAdd(tag, "Yes");
                }

                Features.Add(new { tags });
            }
        }

        public string Title { get; set; }

        public string ImageName { get; set; }
    }
}