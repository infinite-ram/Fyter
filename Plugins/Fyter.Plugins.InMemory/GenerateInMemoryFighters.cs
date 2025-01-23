using Fyter.CoreBusiness.FighterModel;

namespace Fyter.Plugins.InMemory
{
    public static class GenerateInMemoryFighters
    {
        public static List<Fighter> Execute()
        {
            var fighters = new List<Fighter>();
            var random = new Random();

            // Generate a shuffled list of values from 1.0 to 5.0 in 0.5 increments
            List<double> GenerateShuffledStars()
            {
                var stars = Enumerable.Range(0, 9).Select(x => 1.0 + (x * 0.5)).ToList();
                return stars.OrderBy(_ => random.Next()).ToList(); // Shuffle the list
            }

            int totalFighters = 200;
            int alterEgoFrequency = 10; // Every 10th fighter is an alter ego

            for (int i = 1; i <= totalFighters; i++)
            {
                var fighterStyleValues = Enum.GetValues(typeof(FighterStyleEnum));
                var randomFighterStyle = (FighterStyleEnum)
                    fighterStyleValues.GetValue(random.Next(fighterStyleValues.Length));

                // Get random stars for attributes
                var fighterStars = GenerateShuffledStars()[i % 9]; // Randomized FighterStars
                var standUpStars = GenerateShuffledStars()[i % 9]; // Randomized FighterStandUpStars
                var grapplingStars = GenerateShuffledStars()[i % 9]; // Randomized FighterGrapplingStars
                var healthStars = GenerateShuffledStars()[i % 9]; // Randomized FighterHealthStars

                // Random stance
                var stanceValues = Enum.GetValues(typeof(StanceEnum));
                var randomStance = (StanceEnum)
                    stanceValues.GetValue(random.Next(stanceValues.Length));

                Fighter player;

                // Determine if this fighter is an alter ego
                bool isAlterEgo = i % alterEgoFrequency == 0 && fighters.Count > 0;

                if (isAlterEgo)
                {
                    // Create an alter ego for an existing base fighter
                    var baseFighter = fighters[random.Next(fighters.Count)];

                    player = new Fighter
                    {
                        FighterId = i,
                        FirstName = $"{baseFighter.FirstName} (Alter Ego)",
                        LastName = $"{baseFighter.LastName} (Alter Ego)",
                        EgoName = $"Ego {i}",
                        FighterStars = fighterStars,
                        FighterStandUpStars = standUpStars,
                        FighterGrapplingStars = grapplingStars,
                        FighterHealthStars = healthStars,
                        FighterStyle = randomFighterStyle,
                        IsAlterEgo = true,
                        BaseFighterId = baseFighter.FighterId,
                        StandUp = CloneStandUp(baseFighter.StandUp),
                        Grappling = CloneGrappling(baseFighter.Grappling),
                        Health = CloneHealth(baseFighter.Health),
                        FighterInfo = new FighterInfo
                        {
                            NickName = baseFighter.FighterInfo.NickName,
                            Age = baseFighter.FighterInfo.Age,
                            Height = new FighterHeight
                            {
                                Feet = baseFighter.FighterInfo.Height.Feet,
                                Inches = baseFighter.FighterInfo.Height.Inches,
                            },
                            Weight = baseFighter.FighterInfo.Weight,
                            Reach = baseFighter.FighterInfo.Reach,
                            Stance = baseFighter.FighterInfo.Stance,
                            HomeTown = baseFighter.FighterInfo.HomeTown,
                            FightingOutOf = baseFighter.FighterInfo.FightingOutOf,
                        },
                        Division = baseFighter.Division,
                        Perks = new List<PerksEnum>(baseFighter.Perks),
                        TopMoves = baseFighter
                            .TopMoves.Select(tm => new TopMoves
                            {
                                MoveName = tm.MoveName,
                                Stars = tm.Stars,
                            })
                            .ToList(),
                    };
                }
                else
                {
                    // Create a base fighter
                    player = new Fighter
                    {
                        FighterId = i,
                        FirstName = $"Fighter {i}",
                        LastName = $"Fighter {i}",
                        EgoName = $"Ego {i}",
                        FighterStars = fighterStars,
                        FighterStandUpStars = standUpStars,
                        FighterGrapplingStars = grapplingStars,
                        FighterHealthStars = healthStars,
                        FighterStyle = randomFighterStyle,
                        IsAlterEgo = false,
                        StandUp = new StandUp
                        {
                            PunchSpeed = new Stat { Value = 80 + (i % 20), Stars = 3.0 + (i % 2) },
                            PunchPower = new Stat { Value = 75 + (i % 25), Stars = 3.0 + (i % 2) },
                            Accuracy = new Stat { Value = 78 + (i % 22), Stars = 3.0 + (i % 2) },
                            Blocking = new Stat { Value = 76 + (i % 24), Stars = 3.0 + (i % 2) },
                            HeadMovement = new Stat
                            {
                                Value = 77 + (i % 23),
                                Stars = 3.0 + (i % 2),
                            },
                            FootWork = new Stat { Value = 79 + (i % 21), Stars = 3.0 + (i % 2) },
                            SwitchStance = new Stat
                            {
                                Value = 74 + (i % 26),
                                Stars = 3.0 + (i % 2),
                            },
                            TakedownDefense = new Stat
                            {
                                Value = 73 + (i % 27),
                                Stars = 3.0 + (i % 2),
                            },
                            KickPower = new Stat { Value = 81 + (i % 19), Stars = 3.0 + (i % 2) },
                            KickSpeed = new Stat { Value = 82 + (i % 18), Stars = 3.0 + (i % 2) },
                        },
                        Grappling = new Grappling
                        {
                            TakeDowns = new Stat { Value = 70 + (i % 30), Stars = 3.0 + (i % 2) },
                            TopControl = new Stat { Value = 71 + (i % 29), Stars = 3.0 + (i % 2) },
                            BottomControl = new Stat
                            {
                                Value = 72 + (i % 28),
                                Stars = 3.0 + (i % 2),
                            },
                            SubmissionOffense = new Stat
                            {
                                Value = 69 + (i % 31),
                                Stars = 3.0 + (i % 2),
                            },
                            SubmissionDefense = new Stat
                            {
                                Value = 68 + (i % 32),
                                Stars = 3.0 + (i % 2),
                            },
                            GroundStriking = new Stat
                            {
                                Value = 67 + (i % 33),
                                Stars = 3.0 + (i % 2),
                            },
                            ClinchControl = new Stat
                            {
                                Value = 66 + (i % 34),
                                Stars = 3.0 + (i % 2),
                            },
                            ClinchStriking = new Stat
                            {
                                Value = 65 + (i % 35),
                                Stars = 3.0 + (i % 2),
                            },
                        },
                        Health = new Health
                        {
                            Cardio = new Stat { Value = 85 + (i % 15), Stars = 3.0 + (i % 2) },
                            Chin = new Stat { Value = 85 + (i % 15), Stars = 3.0 + (i % 2) },
                            Body = new Stat { Value = 84 + (i % 16), Stars = 3.0 + (i % 2) },
                            Legs = new Stat { Value = 83 + (i % 17), Stars = 3.0 + (i % 2) },
                            Recovery = new Stat { Value = 82 + (i % 18), Stars = 3.0 + (i % 2) },
                            CutResistant = new Stat
                            {
                                Value = 81 + (i % 19),
                                Stars = 3.0 + (i % 2),
                            },
                        },
                        FighterInfo = new FighterInfo
                        {
                            NickName = $"Nickname {i}",
                            Age = 20 + (i % 15),
                            Height = new FighterHeight { Feet = 5, Inches = 5 },
                            Weight = 150 + (i % 50),
                            Reach = 70 + (i % 10),
                            Stance = randomStance,
                            HomeTown = $"Hometown {i}",
                            FightingOutOf = $"City {i}",
                        },
                        Division = (DivisionEnum)(i % Enum.GetValues(typeof(DivisionEnum)).Length),
                        Perks = new List<PerksEnum>
                        {
                            (PerksEnum)(i % Enum.GetValues(typeof(PerksEnum)).Length),
                            (PerksEnum)((i + 1) % Enum.GetValues(typeof(PerksEnum)).Length),
                            (PerksEnum)((i + 2) % Enum.GetValues(typeof(PerksEnum)).Length),
                        },
                        TopMoves = new List<TopMoves>
                        {
                            new TopMoves { MoveName = $"Move {i}A", Stars = 3 + (i % 3) },
                            new TopMoves { MoveName = $"Move {i}B", Stars = 3 + (i % 3) },
                        },
                    };
                }

                fighters.Add(player);
            }

            return fighters;
        }

        // Helper methods to clone stats for alter egos
        private static StandUp CloneStandUp(StandUp original)
        {
            return new StandUp
            {
                PunchSpeed = new Stat
                {
                    Value = original.PunchSpeed.Value,
                    Stars = original.PunchSpeed.Stars,
                },
                PunchPower = new Stat
                {
                    Value = original.PunchPower.Value,
                    Stars = original.PunchPower.Stars,
                },
                Accuracy = new Stat
                {
                    Value = original.Accuracy.Value,
                    Stars = original.Accuracy.Stars,
                },
                Blocking = new Stat
                {
                    Value = original.Blocking.Value,
                    Stars = original.Blocking.Stars,
                },
                HeadMovement = new Stat
                {
                    Value = original.HeadMovement.Value,
                    Stars = original.HeadMovement.Stars,
                },
                FootWork = new Stat
                {
                    Value = original.FootWork.Value,
                    Stars = original.FootWork.Stars,
                },
                SwitchStance = new Stat
                {
                    Value = original.SwitchStance.Value,
                    Stars = original.SwitchStance.Stars,
                },
                TakedownDefense = new Stat
                {
                    Value = original.TakedownDefense.Value,
                    Stars = original.TakedownDefense.Stars,
                },
                KickPower = new Stat
                {
                    Value = original.KickPower.Value,
                    Stars = original.KickPower.Stars,
                },
                KickSpeed = new Stat
                {
                    Value = original.KickSpeed.Value,
                    Stars = original.KickSpeed.Stars,
                },
            };
        }

        private static Grappling CloneGrappling(Grappling original)
        {
            return new Grappling
            {
                TakeDowns = new Stat
                {
                    Value = original.TakeDowns.Value,
                    Stars = original.TakeDowns.Stars,
                },
                TopControl = new Stat
                {
                    Value = original.TopControl.Value,
                    Stars = original.TopControl.Stars,
                },
                BottomControl = new Stat
                {
                    Value = original.BottomControl.Value,
                    Stars = original.BottomControl.Stars,
                },
                SubmissionOffense = new Stat
                {
                    Value = original.SubmissionOffense.Value,
                    Stars = original.SubmissionOffense.Stars,
                },
                SubmissionDefense = new Stat
                {
                    Value = original.SubmissionDefense.Value,
                    Stars = original.SubmissionDefense.Stars,
                },
                GroundStriking = new Stat
                {
                    Value = original.GroundStriking.Value,
                    Stars = original.GroundStriking.Stars,
                },
                ClinchControl = new Stat
                {
                    Value = original.ClinchControl.Value,
                    Stars = original.ClinchControl.Stars,
                },
                ClinchStriking = new Stat
                {
                    Value = original.ClinchStriking.Value,
                    Stars = original.ClinchStriking.Stars,
                },
            };
        }

        private static Health CloneHealth(Health original)
        {
            return new Health
            {
                Cardio = new Stat { Value = original.Cardio.Value, Stars = original.Cardio.Stars },
                Chin = new Stat { Value = original.Chin.Value, Stars = original.Chin.Stars },
                Body = new Stat { Value = original.Body.Value, Stars = original.Body.Stars },
                Legs = new Stat { Value = original.Legs.Value, Stars = original.Legs.Stars },
                Recovery = new Stat
                {
                    Value = original.Recovery.Value,
                    Stars = original.Recovery.Stars,
                },
                CutResistant = new Stat
                {
                    Value = original.CutResistant.Value,
                    Stars = original.CutResistant.Stars,
                },
            };
        }
    }
}
