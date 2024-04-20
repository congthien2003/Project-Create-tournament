using CreateTournament.Models;

namespace CreateTournament.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                context.Database.EnsureCreated();
                if(!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>
                    {
                        new User()
                        {
                            Username = "NhatDaika",
                            Email = "nhatnm2003@gmail.com",
                            Phones = "0123456789",
                            Password = "Nhat2003@",
                            Role = 1,
                            IsDeleted = false
                        }
                    });
                    context.SaveChanges();
                }
                var userId = context.Users.Where(obj=> obj.Username.Equals("NhatDaika")).Select(obj=>obj.Id).FirstOrDefault();
                if(!context.FormatTypes.Any())
                {
                    context.FormatTypes.AddRange(new List<FormatType>
                    {
                        new FormatType()
                        {
                            Name = "Knock Out"
                        }
                    });
                    context.SaveChanges();
                }
                var formatTypeId = context.FormatTypes.Where(obj => obj.Name.Equals("Knock Out")).Select(obj => obj.Id).FirstOrDefault();
                if (!context.SportTypes.Any())
                {
                    context.SportTypes.AddRange(new List<SportType>
                    {
                        new SportType()
                        {
                            Name = "Football"
                        }
                    });
                    context.SaveChanges();
                }
                var sportTypeId = context.SportTypes.Where(obj => obj.Name.Equals("Football")).Select(obj => obj.Id).FirstOrDefault();
                if (!context.Tournaments.Any())
                {
                    context.Tournaments.AddRange(new List<Tournament>
                    {
                        new Tournament()
                        {
                            UserId = userId,
                            Name = "Champion League",
                            Location = "TPHCM",
                            QuantityTeam = 4,
                            FormatTypeId = formatTypeId,
                            SportTypeId = sportTypeId,
                            Created = DateTime.Now,
                            IsDeleted = false
                        }
                    });
                    context.SaveChanges();
                }
                var touramentId = context.Tournaments.Where(obj => obj.Name.Equals("Champion League")).Select(obj =>obj.Id).FirstOrDefault();
                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(new List<Team>
                    {
                        new Team()
                        {
                            Name = "Real Madrid",
                            TournamentId = touramentId,
                            IsDeleted =false
                        },
                        new Team()
                        {
                            Name = "PSG",
                            TournamentId = touramentId,
                            IsDeleted =false
                        },
                        new Team()
                        {
                            Name = "Bayern Munich",
                            TournamentId = touramentId,
                            IsDeleted =false
                        },
                        new Team()
                        {
                            Name = "DortMund",
                            TournamentId = touramentId,
                            IsDeleted =false
                        }
                    });
                    context.SaveChanges();
                }
                var IdTeam1 = context.Teams.Where(obj => obj.Name.Equals("Real Madrid")).Select(obj => obj.Id).FirstOrDefault();
                var IdTeam2 = context.Teams.Where(obj => obj.Name.Equals("PSG")).Select(obj => obj.Id).FirstOrDefault();
                var IdTeam3 = context.Teams.Where(obj => obj.Name.Equals("Bayern Munich")).Select(obj => obj.Id).FirstOrDefault();
                var IdTeam4 = context.Teams.Where(obj => obj.Name.Equals("DortMund")).Select(obj => obj.Id).FirstOrDefault();
                if (!context.Players.Any())
                {
                    context.Players.AddRange(new List<Player>()
                    {
                        new Player()
                        {
                            Name = "Vinicius",
                            TeamId = IdTeam1
                        },
                        new Player()
                        {
                            Name = "Kroos",
                            TeamId = IdTeam1
                        },
                        new Player()
                        {
                            Name = "Mbappe",
                            TeamId = IdTeam2
                        },
                        new Player()
                        {
                            Name = "Dembele",
                            TeamId = IdTeam2
                        },
                        new Player()
                        {
                            Name = "Gnarby",
                            TeamId = IdTeam3
                        },
                        new Player()
                        {
                            Name = "Muller",
                            TeamId = IdTeam3
                        },
                        new Player()
                        {
                            Name = "Sancho",
                            TeamId = IdTeam4
                        },
                        new Player()
                        {
                            Name = "Reus",
                            TeamId = IdTeam4
                        },
                    });
                    context.SaveChanges();
                }
                var IdPlayer1 = context.Players.Where(obj => obj.Name.Equals("Vinicius")).Select(obj => obj.Id).FirstOrDefault();
                var IdPlayer2 = context.Players.Where(obj => obj.Name.Equals("Kroos")).Select(obj => obj.Id).FirstOrDefault();
                var IdPlayer3 = context.Players.Where(obj => obj.Name.Equals("Mbappe")).Select(obj => obj.Id).FirstOrDefault();
                var IdPlayer4 = context.Players.Where(obj => obj.Name.Equals("Dembele")).Select(obj => obj.Id).FirstOrDefault();
                var IdPlayer5 = context.Players.Where(obj => obj.Name.Equals("Gnarby")).Select(obj => obj.Id).FirstOrDefault();
                var IdPlayer6 = context.Players.Where(obj => obj.Name.Equals("Muller")).Select(obj => obj.Id).FirstOrDefault();
                var IdPlayer7 = context.Players.Where(obj => obj.Name.Equals("Sancho")).Select(obj => obj.Id).FirstOrDefault();
                var IdPlayer8 = context.Players.Where(obj => obj.Name.Equals("Reus")).Select(obj => obj.Id).FirstOrDefault();
                if (!context.Matches.Any())
                {
                    context.Matches.AddRange(new List<Match>
                    {
                        new Match()
                        {
                            IdTeam1 = IdTeam1,
                            IdTeam2 = IdTeam3,
                            TouramentId = touramentId,
                            StartAt = DateTime.Now,
                            Created = DateTime.Now,
                            IsDeleted = false
                            
                        },
                        new Match()
                        {
                            IdTeam1 = IdTeam2,
                            IdTeam2 = IdTeam4,
                            TouramentId = touramentId,
                            StartAt = DateTime.Now,
                            Created = DateTime.Now,
                            IsDeleted = false
                        },
                        new Match()
                        {
                            IdTeam1 = IdTeam1,
                            IdTeam2 = IdTeam4,
                            TouramentId = touramentId,
                            StartAt = DateTime.Now,
                            Created = DateTime.Now,
                            IsDeleted = false
                        },
                    });
                    context.SaveChanges();
                }
                if(!context.MatchResults.Any())
                {
                    context.MatchResults.AddRange(new List<MatchResult>
                    {
                        new MatchResult()
                        {
                            MatchId = 1,
                            ScoreT1 = 3,
                            ScoreT2 = 2,
                            IdTeamWin = IdTeam1,
                            Finish = DateTime.Now,
                            IsDeleted =false
                        },
                        new MatchResult()
                        {
                            MatchId = 2,
                            ScoreT1 = 0,
                            ScoreT2 = 2,
                            IdTeamWin = IdTeam4,
                            Finish = DateTime.Now,
                            IsDeleted =false
                        },
                        new MatchResult()
                        {
                            MatchId = 3,
                            ScoreT1 = 3,
                            ScoreT2 = 1,
                            IdTeamWin = IdTeam1,
                            Finish = DateTime.Now,
                            IsDeleted =false
                        }
                    });
                    context.SaveChanges();
                }
                if(!context.PlayerStats.Any())
                {
                    context.PlayerStats.AddRange(new List<PlayerStats>
                    {
                        new PlayerStats()
                        {
                            MatchResultId = 1,
                            PlayerId = IdPlayer1,
                            Score = 2,
                            Assits = 1,
                            RedCard = 0,
                            YellowCard = 0,
                        },
                        new PlayerStats()
                        {
                            MatchResultId = 1,
                            PlayerId = IdPlayer2,
                            Score = 1,
                            Assits = 2,
                            RedCard = 0,
                            YellowCard = 1,
                        },
                        new PlayerStats()
                        {
                            MatchResultId = 1,
                            PlayerId = IdPlayer5,
                            Score = 2,
                            Assits = 0,
                            RedCard = 0,
                            YellowCard = 0,
                        },
                        new PlayerStats()
                        {
                            MatchResultId = 1,
                            PlayerId = IdPlayer6,
                            Score = 0,
                            Assits = 2,
                            RedCard = 0,
                            YellowCard = 0,
                        },
                        new PlayerStats()
                        {
                            MatchResultId = 2,
                            PlayerId = IdPlayer3,
                            Score = 0,
                            Assits = 0,
                            RedCard = 0,
                            YellowCard = 1,
                        },
                        new PlayerStats()
                        {
                            MatchResultId = 2,
                            PlayerId = IdPlayer4,
                            Score = 0,
                            Assits = 0,
                            RedCard = 0,
                            YellowCard = 0,
                        },
                        new PlayerStats()
                        {
                            MatchResultId = 2,
                            PlayerId = IdPlayer7,
                            Score = 2,
                            Assits = 0,
                            RedCard = 0,
                            YellowCard = 0,
                        },
                        new PlayerStats()
                        {
                            MatchResultId = 2,
                            PlayerId = IdPlayer8,
                            Score = 0,
                            Assits = 2,
                            RedCard = 0,
                            YellowCard = 0,
                        },
                        new PlayerStats()
                        {
                            MatchResultId = 3,
                            PlayerId = IdPlayer1,
                            Score = 1,
                            Assits = 2,
                            RedCard = 0,
                            YellowCard = 0,
                        },
                        new PlayerStats()
                        {
                            MatchResultId = 3,
                            PlayerId = IdPlayer2,
                            Score = 2,
                            Assits = 1,
                            RedCard = 0,
                            YellowCard = 0,
                        },
                        new PlayerStats()
                        {
                            MatchResultId = 3,
                            PlayerId = IdPlayer8,
                            Score = 1,
                            Assits = 0,
                            RedCard = 0,
                            YellowCard = 0,
                        },
                        new PlayerStats()
                        {
                            MatchResultId = 3,
                            PlayerId = IdPlayer8,
                            Score = 0,
                            Assits = 1,
                            RedCard = 0,
                            YellowCard = 0,
                        },
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
