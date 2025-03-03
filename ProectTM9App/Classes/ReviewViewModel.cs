using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProectTM9App.Classes
{
    public class ReviewViewModel
    {
        public ObservableCollection<Review> Reviews { get; set; }

        private const string FilePath = "reviews.json";

        public ReviewViewModel()
        {
            Reviews = new ObservableCollection<Review>();
            LoadReviews();
        }

        public void SaveReviews()
        {
            var json = JsonConvert.SerializeObject(Reviews, Formatting.Indented);
            File.WriteAllText("reviews.json", json);
        }

        public void LoadReviews()
        {
            if (File.Exists("reviews.json"))
            {
                var json = File.ReadAllText("reviews.json");
                var reviews = JsonConvert.DeserializeObject<ObservableCollection<Review>>(json);
                if (reviews != null)
                {
                    Reviews.Clear();
                    foreach (var review in reviews)
                    {
                        Reviews.Add(review);
                    }
                }
            }
        }
    }
}
