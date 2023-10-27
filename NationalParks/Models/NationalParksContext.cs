using Microsoft.EntityFrameworkCore;

namespace NationalParks.Models
{
  public class NationalParksContext : DbContext
  {
    
    public DbSet<Park> Parks { get; set; }
    public NationalParksContext(DbContextOptions<NationalParksContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Park>()
      .HasData(
        new Park { ParkId = 1, Name = "Yosemite National Park", State = "California", Description = "Yosemite is known for its waterfalls, deep valleys, grand meadows, and ancient giant sequoias.", AnnualVisitors = 4000000 },
        new Park { ParkId = 2, Name = "Sequoia and Kings Canyon National Parks", State = "California", Description = "These parks are home to giant sequoias, rugged mountains, and deep canyons.", AnnualVisitors = 1500000 },
        new Park { ParkId = 3, Name = "Joshua Tree National Park", State = "California", Description = "Joshua Tree is famous for its unique desert landscapes and iconic Joshua trees.", AnnualVisitors = 3000000 },
        new Park { ParkId = 4, Name = "Redwood National and State Parks", State = "California", Description = "Home to the tallest trees on Earth, the coastal redwoods.", AnnualVisitors = 800000 },
        new Park { ParkId = 5, Name = "Lassen Volcanic National Park", State = "California", Description = "Lassen features volcanic landscapes, hot springs, and pristine mountain lakes.", AnnualVisitors = 250000 },
        new Park { ParkId = 6, Name = "Silver State Falls", State = "Oregon", Description = "Silver State Falls is a beautiful park featuring stunning waterfalls, lush forests, and hiking trails.", AnnualVisitors = 500000 },
        new Park { ParkId = 7, Name = "Acadia National Park", State = "Maine", Description = "Acadia features the tallest mountain on the Atlantic coast of the United States, granite peaks, ocean shoreline, woodlands, and lakes.", AnnualVisitors = 3500000 },
        new Park { ParkId = 8, Name = "Great Smoky Mountains National Park", State = "North Carolina/Tennessee", Description = "Great Smoky Mountains is known for its mist-covered mountains, diverse plant and animal life, and historic cabins.", AnnualVisitors = 12000000 },
        new Park { ParkId = 9, Name = "Zion National Park", State = "Utah", Description = "Zion is famous for its towering red rock formations and deep canyons.", AnnualVisitors = 4500000 },
        new Park { ParkId = 10, Name = "Grand Canyon National Park", State = "Arizona", Description = "The Grand Canyon is a mile-deep, breathtaking gorge carved by the Colorado River.", AnnualVisitors = 6000000 },
        new Park { ParkId = 11, Name = "Yellowstone National Park", State = "Wyoming", Description = "Yellowstone is known for its geothermal features, wildlife, and dramatic landscapes.", AnnualVisitors = 4200000 },
        new Park { ParkId = 12, Name = "Everglades National Park", State = "Florida", Description = "The Everglades is a vast wetland with unique ecosystems and diverse wildlife.", AnnualVisitors = 1000000 }
      );
    }
  }
}