using System.Collections.Generic;
using Adventure.Feature.EventDetailsProvider.Constants;
using Adventure.Feature.EventDetailsProvider.Utilities.Interfaces;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Constants;

namespace Adventure.Feature.EventDetailsProvider.Utilities
{
    public class EventDataProvider : IEventDataProvider
    {
        private Dictionary<string, List<string>> _data;

        public Dictionary<string, List<string>> Data
        {
            get
            {
                if (_data is null)
                {
                    _data = InitializeData();
                }

                return _data;
            }
        }

        private Dictionary<string, List<string>> InitializeData()
        {
            return new Dictionary<string, List<string>>
            {
                {
                    $"{EventDetailsPage.Fields.Title_FieldName}{EventSourceSections.Beginning}", new List<string>
                    {
                        "Incredible", "Fantastic", "Unreal", "Exciting", "Impressive"
                    }
                },
                {
                    $"{EventDetailsPage.Fields.Title_FieldName}{EventSourceSections.Center}", new List<string>
                    {
                        "journey", "trip", "excursion", "flight", "hike"
                    }
                },
                {
                    $"{EventDetailsPage.Fields.Title_FieldName}{EventSourceSections.Ending}", new List<string>
                    {
                        "to Everest", "to Carpathians" , "to Arctic", "to the Moon", "to the Paris"
                    }
                },
                {
                    $"{EventDetailsPage.Fields.Description_FieldName}{EventSourceSections.Beginning}", new List<string>
                    {
                        "Travel is one of most people's favorite activities. Why do many people love to travel so much? It's simple, when a person travels, he gets to know the world around him and himself.\n",
                        "Everyone should rest at least once a year, so when you start to spend sleepless nights thinking about the sun and the sea, when you think longingly about the green countryside, there is no longer any doubt - you are ready for the rest. You need to move away from all your everyday problems and get some fresh air.\n",
                        "Traveling is very popular nowadays. A lot of people travel to different countries if they have such opportunity. Travelling allows you to get interesting experience, learn about people’s life in other countries and continents. I think it is very interesting to discover new things, new places and different ways of life. While on travel, you meet new people, try different meals; see world famous places with your own eyes.\n",
                        "Travel is always something new and interesting, it is a meeting with new people, cities and sights. Travel always gives new emotions and impressions, it allows you to escape from everyday life and plunge into a new other life, at least for a few days.\n",
                        "What could be better and more beautiful than travel? Surely nothing! After all, travel is about gaining new experience, meeting wonderful people, visiting wonderful places about which, perhaps, I have ever read in books! When traveling, a person certainly educates himself, learns new languages for himself, learns the secrets of the culinary of this or that nation!\n"
                    }
                },
                {
                    $"{EventDetailsPage.Fields.Description_FieldName}{EventSourceSections.Center}", new List<string>
                    {
                        "Paris is a political, administrative and cultural centre of the country. The University of Paris or Sorbonne, founded in the 12th century, is one of its original medieval colleges and one of the best and most prestigious universities in the world. Paris is recognized as the educational centre of the country and the major part of schools and universities is located there. The population of the city is multifarious: Europeans, black people, Arabs. Paris is a motherland of numerous magazines, newspapers and publications.\n",
                        "Mount Everest is the highest peak in the world and is located in the Himalayas. Its height is 8848 m. The mountain is divided by the border of Nepal and China (Tibet Autonomous Region), but its peak lies on the territory of China. Everest has the shape of a pyramid. Glaciers descend from its slopes in all directions, ending at an altitude of about 5 thousand meters.\n",
                        "The moon is the only natural satellite of the earth. The closest satellite of the planet to the Sun, since the planets closest to the Sun do not have them. The second brightest object in the earth's sky after the Sun and the fifth largest natural satellite of the planet of the solar system. The average distance between the centers of the Earth and the Moon is 384 467 km.\n",
                        "The Arctic is a single physical and geographical region of the Earth, adjacent to the North Pole and including the outskirts of the continents of Eurasia and North America, almost the entire Arctic Ocean with islands, as well as the adjacent parts of the Atlantic and Pacific oceans. The southern border of the Arctic coincides with the southern border of the tundra zone.\n",
                        "The Carpathians are a mountain system with a length of about 1500 kilometers in Central and Eastern Europe. They stretch in an arc from west to east from the Czech Republic to Romania. National parks have been organized on the territory of the Tatra mountain range between Slovakia and Poland, which has several peaks over 2400 meters high. The spruce forests of the Carpathians, most of which are in Romania, are home to brown bears, wolves and lynxes.\n"
                    }
                },
                {
                    $"{EventDetailsPage.Fields.Description_FieldName}{EventSourceSections.Ending}", new List<string>
                    {
                        "Your Hotel Ambassador Tre Rose is cozy and small, enveloped in a relaxed atmosphere in which every guest will feel comfortable, the Venice Hotel has common areas - hall, administration and bar - refurbished by the skillful hand of the Venetian architect Carlo Scarpa.",
                        "The hotel offers cozy comfortable double rooms and superior rooms that meet European standards. The elegant interiors of the hotel are made in warm light colors, and the equipment meets all modern requirements. Our hotel has 17 rooms of various categories, including DeLuxe rooms. The hotel combines the comfort of home and the comfort of a modern setting." ,
                        "Mini-hotel \"Vintage\" opened its doors in July 2010.The Vintage Hotel is a new business class mini-hotel that successfully combines the best traditions of European service and the flavor of the old Russian city.The Vintage Hotel is unique in its architecture and location in the business and cultural center of the historical part of the city, at the beginning of the pedestrian zone of one of the oldest streets in Kaluga - st.Teatralnaya, which is a traditional resting place for Kaluga residents and guests of the city, where there are an abundance of cafes, restaurants, shops, and live music sounds in the evenings.",
                        "Hotel Karpaty. The hotel has 257 rooms of various categories: economy, standard, two-room standard, 3-local economy, 4-local economy, comfort, junior suites and suites. Everyone can choose the type of accommodation according to their preferences: one-room or two-room suite, with or without a balcony, overlooking the street or courtyard, with shared or separate beds, with or without extra bed. All rooms, regardless of category, are equipped in accordance with modern service standards: each has a bathroom with a bath or shower, flat-screen TV, telephone, refrigerator and bathroom accessories.",
                        "A modern and elegant hotel, recently renovated and refurbished, ideal for both business and leisure travelers, it contains 34 cozy rooms and is a 5-minute walk from the train station. All rooms are equipped with modern showers and toilets, TV with satellite channels, W-Lan, radio, telephone, safe. For a successful start to the day, guests are offered a buffet breakfast. In the immediate vicinity of the hotel there is the \"old town\" with \"the longest bar in the world\", the Royal Alley, musicals and theaters, exhibitions and the international airport, which can be easily reached by public transport."
                    }
                },
                {
                    $"{EventDetailsPage.Fields.ButtonText_FieldName}{EventSourceSections.Beginning}", new List<string>
                    {
                        "Submit", "Press", "Click", "Move", ""
                    }
                },
                {
                    $"{EventDetailsPage.Fields.ButtonText_FieldName}{EventSourceSections.Center}", new List<string>
                    {
                        "Me", "", "Here", "On", "Forward"
                    }
                },
                {
                    $"{EventDetailsPage.Fields.ButtonText_FieldName}{EventSourceSections.Ending}", new List<string>
                    {
                        "Better", "Faster" , "Stronger", "For Sure", "Greater"
                    }
                },
                {
                    $"{EventDetailsPage.Fields.ShortDescription_FieldName}{EventSourceSections.Beginning}", new List<string>
                    {
                        "Travel is one of most people's favorite activities.",
                        "Everyone should rest at least once a year.",
                        "Traveling is very popular nowadays.",
                        "Travel is always something new and interesting.",
                        "What could be better and more beautiful than travel?"
                    }
                },
                {
                    $"{EventDetailsPage.Fields.ShortDescription_FieldName}{EventSourceSections.Center}", new List<string>
                    {
                        "Paris is a political, administrative and cultural centre of the country.",
                        "Mount Everest is the highest peak in the world and is located in the Himalayas.",
                        "The moon is the only natural satellite of the earth.",
                        "The Arctic is a single physical and geographical region of the Earth.",
                        "The Carpathians are a mountain system with a length of about 1500 kilometers."
                    }
                },
                {
                    $"{EventDetailsPage.Fields.ShortDescription_FieldName}{EventSourceSections.Ending}", new List<string>
                    {
                        "Your Hotel Ambassador Tre Rose is cozy and small, enveloped in a relaxed atmosphere.",
                        "The hotel offers cozy comfortable double rooms and superior rooms that meet European standards." ,
                        "Mini-hotel \"Vintage\" opened its doors in July 2010.",
                        "Hotel Karpaty. The hotel has 257 rooms of various categories: economy, standard, two-room standard, 3-local economy, 4-local economy, comfort, junior suites and suites.",
                        "A modern and elegant hotel, recently renovated and refurbished."
                    }
                },
            };
        }
    }
}
