using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HelloWorldMod.NPCs
{
	public class BennettOnAUnicorn : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bennett on a Unicorn");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Unicorn];
		}

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Unicorn);
			aiType = NPCID.Unicorn;
			animationType = NPCID.Unicorn;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 1f;
        }
    }
}