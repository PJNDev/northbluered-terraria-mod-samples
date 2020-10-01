using Terraria.ID;
using Terraria.ModLoader;

namespace HelloWorldMod.Items
{
	public class Whetstone : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("HelloWorldSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Sharpens melee weapons.");
		}

		public override void SetDefaults() 
		{
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.buffType = BuffID.Sharpened;
			item.buffTime = 4 * 60 * 60; //4 minutes
			item.UseSound = SoundID.Item37;
			item.consumable = true;
			item.maxStack = 30;
			item.useTime = 30;
			item.useAnimation = 30;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronOre, 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LeadOre, 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}