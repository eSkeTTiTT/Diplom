using Diplom.DOMAIN.NewModels;
using Diplom.DOMAIN;
using Microsoft.EntityFrameworkCore;

namespace KeypointMatching.Recommender;

public class UserBehaviorDatabase
{
	public DbSet<Person> Persons { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<UserActions> UserActions { get; set; }
}