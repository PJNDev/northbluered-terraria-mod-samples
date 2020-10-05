//using Microsoft.Xna.Framework;
//using System.IO;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using static Terraria.ModLoader.ModContent;

//namespace ExampleMod.NPCs
//{
//	internal class WyvernSampleHead : WyvernSample
//	{
//		public override string Texture => "Terraria/NPC_" + NPCID.WyvernHead;

//		public override void SetDefaults()
//		{
//			npc.CloneDefaults(NPCID.WyvernHead);
//			npc.aiStyle = -1;
//		}

//		public override void Init()
//		{
//			base.Init();
//			isHead = true;
//		}

//		private int attackCounter;
//		public override void SendExtraAI(BinaryWriter writer)
//		{
//			writer.Write(attackCounter);
//		}

//		public override void ReceiveExtraAI(BinaryReader reader)
//		{
//			attackCounter = reader.ReadInt32();
//		}
//	}

//    internal class WyvernSampleNeck : WyvernSample
//    {
//        public override string Texture => "Terraria/NPC_" + NPCID.WyvernBody;

//        public override void SetDefaults()
//        {
//            npc.CloneDefaults(NPCID.WyvernBody);
//            npc.aiStyle = -1;
//        }
//        public override void Init()
//        {
//            base.Init();
//            isNeck = true;
//        }
//    }

//    internal class WyvernSampleLegs : WyvernSample
//    {
//        public override string Texture => "Terraria/NPC_" + NPCID.WyvernLegs;

//        public override void SetDefaults()
//        {
//            npc.CloneDefaults(NPCID.WyvernBody);
//            npc.aiStyle = -1;
//        }

//        public override void Init()
//        {
//            base.Init();
//            isLegs = true;
//        }
//    }

//    internal class WyvernSampleBody : WyvernSample
//	{
//		public override string Texture => "Terraria/NPC_" + NPCID.WyvernBody;

//		public override void SetDefaults()
//		{
//			npc.CloneDefaults(NPCID.WyvernBody);
//			npc.aiStyle = -1;
//		}
//	}

//    internal class WyvernSampleTailStart : WyvernSample
//    {
//        public override string Texture => "Terraria/NPC_" + NPCID.WyvernBody2;

//        public override void SetDefaults()
//        {
//            npc.CloneDefaults(NPCID.WyvernBody);
//            npc.aiStyle = -1;
//        }
//    }

//    internal class WyvernSampleTailMiddle : WyvernSample
//    {
//        public override string Texture => "Terraria/NPC_" + NPCID.WyvernBody3;

//        public override void SetDefaults()
//        {
//            npc.CloneDefaults(NPCID.WyvernBody);
//            npc.aiStyle = -1;
//        }
//    }

//    internal class WyvernSampleTailEnd : WyvernSample
//	{
//		public override string Texture => "Terraria/NPC_" + NPCID.WyvernTail;

//		public override void SetDefaults()
//		{
//			npc.CloneDefaults(NPCID.WyvernTail);
//			npc.aiStyle = -1;
//		}

//		public override void Init()
//		{
//			base.Init();
//			isTailEnd = true;
//		}
//	}

//	// I made this 2nd base class to limit code repetition.
//	public abstract class WyvernSample : Worm
//	{
//		public override void SetStaticDefaults()
//		{
//			DisplayName.SetDefault("Sample, Wyvern Rider");
//		}

//		public override void Init()
//		{
//			minLength = 8;
//			maxLength = 12;
//            headType = NPCType<WyvernSampleHead>();
//            neckType = NPCType<WyvernSampleNeck>();
//            legsType = NPCType<WyvernSampleLegs>();
//            bodyType = NPCType<WyvernSampleBody>();
//            tailStartType = NPCType<WyvernSampleTailStart>();
//            tailMiddleType = NPCType<WyvernSampleTailMiddle>();
//            tailEndType = NPCType<WyvernSampleTailEnd>();
//			speed = 10f;
//			turnSpeed = 0.045f;
//			flies = true;
//		}

//		public override float SpawnChance(NPCSpawnInfo spawnInfo)
//		{
//			return 1f;
//		}
//	}

//	// This abstract class can be used for non splitting worm type NPC.
//	public abstract class Worm : ModNPC
//	{
//		public bool isHead;
//		public bool isNeck;
//		public bool isLegs;
//		public bool isTailEnd;
//		public int minLength;
//		public int maxLength;
//		public int headType;
//        public int neckType;
//        public int legsType;
//        public int bodyType;
//        public int tailStartType;
//        public int tailMiddleType;
//        public int tailEndType;
//		public bool flies = true;
//		public bool directional = false;
//		public float speed;
//		public float turnSpeed;

//		private float follower { get { return npc.ai[0]; } set { npc.ai[0] = value; } }
//		private float following { get { return npc.ai[1]; } set { npc.ai[1] = value; } }
//		private float distanceFromTail { get { return npc.ai[2]; } set { npc.ai[2] = value; } }
//		private float head { get { return npc.ai[3]; } set { npc.ai[3] = value; } }

//		public override void AI()
//		{
//			if (npc.localAI[1] == 0f)
//			{
//				npc.localAI[1] = 1f;
//				Init();
//			}
//			if (head > 0f)
//			{
//				npc.realLife = (int)head;
//			}
//			if (!isHead && npc.timeLeft < 300)
//			{
//				npc.timeLeft = 300;
//			}
//			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
//			{
//				npc.TargetClosest(true);
//			}
//			if (Main.player[npc.target].dead && npc.timeLeft > 300)
//			{
//				npc.timeLeft = 300;
//			}
//			if (Main.netMode != NetmodeID.MultiplayerClient)
//			{
//				if (!isTailEnd && follower == 0f)
//				{
//					if (isHead)
//					{
//						head = (float)npc.whoAmI;
//						npc.realLife = npc.whoAmI;
//						distanceFromTail = (float)Main.rand.Next(minLength, maxLength + 1);
//						follower = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), neckType, npc.whoAmI);
//						//follower = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), bodyType, npc.whoAmI);
//					}
//					else if (distanceFromTail > 0f)
//					{
//                        if (isNeck)
//                        {
//                            follower = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), legsType, npc.whoAmI);
//                        }
//                        else if (isLegs)
//                        {
//                            follower = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), bodyType, npc.whoAmI);
//                        }
//                        else
//                        {
//							follower = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), npc.type, npc.whoAmI);
//						}
//					}
//					else
//					{
//						follower = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), tailEndType, npc.whoAmI);
//					}
//					Main.npc[(int)follower].ai[3] = head;
//					Main.npc[(int)follower].realLife = npc.realLife;
//					Main.npc[(int)follower].ai[1] = (float)npc.whoAmI;
//					Main.npc[(int)follower].ai[2] = distanceFromTail - 1f;
//					npc.netUpdate = true;
//				}
//				if (!isHead && (!Main.npc[(int)following].active || Main.npc[(int)following].type != headType && Main.npc[(int)following].type != bodyType))
//				{
//					npc.life = 0;
//					npc.HitEffect(0, 10.0);
//					npc.active = false;
//				}
//				if (!isTailEnd && (!Main.npc[(int)follower].active || Main.npc[(int)follower].type != bodyType && Main.npc[(int)follower].type != tailEndType))
//				{
//					npc.life = 0;
//					npc.HitEffect(0, 10.0);
//					npc.active = false;
//				}
//				if (!npc.active && Main.netMode == NetmodeID.Server)
//				{
//					NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
//				}
//			}
//			int num180 = (int)(npc.position.X / 16f) - 1;
//			int num181 = (int)((npc.position.X + (float)npc.width) / 16f) + 2;
//			int num182 = (int)(npc.position.Y / 16f) - 1;
//			int num183 = (int)((npc.position.Y + (float)npc.height) / 16f) + 2;
//			if (num180 < 0)
//			{
//				num180 = 0;
//			}
//			if (num181 > Main.maxTilesX)
//			{
//				num181 = Main.maxTilesX;
//			}
//			if (num182 < 0)
//			{
//				num182 = 0;
//			}
//			if (num183 > Main.maxTilesY)
//			{
//				num183 = Main.maxTilesY;
//			}
//			bool flag18 = flies;
//			if (!flag18)
//			{
//				for (int num184 = num180; num184 < num181; num184++)
//				{
//					for (int num185 = num182; num185 < num183; num185++)
//					{
//						if (Main.tile[num184, num185] != null && (Main.tile[num184, num185].nactive() && (Main.tileSolid[(int)Main.tile[num184, num185].type] || Main.tileSolidTop[(int)Main.tile[num184, num185].type] && Main.tile[num184, num185].frameY == 0) || Main.tile[num184, num185].liquid > 64))
//						{
//							Vector2 vector17;
//							vector17.X = (float)(num184 * 16);
//							vector17.Y = (float)(num185 * 16);
//							if (npc.position.X + (float)npc.width > vector17.X && npc.position.X < vector17.X + 16f && npc.position.Y + (float)npc.height > vector17.Y && npc.position.Y < vector17.Y + 16f)
//							{
//								flag18 = true;
//								if (Main.rand.NextBool(100) && npc.behindTiles && Main.tile[num184, num185].nactive())
//								{
//									WorldGen.KillTile(num184, num185, true, true, false);
//								}
//								if (Main.netMode != NetmodeID.MultiplayerClient && Main.tile[num184, num185].type == 2)
//								{
//									ushort arg_BFCA_0 = Main.tile[num184, num185 - 1].type;
//								}
//							}
//						}
//					}
//				}
//			}
//			if (!flag18 && isHead)
//			{
//				Rectangle rectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
//				int num186 = 1000;
//				bool flag19 = true;
//				for (int num187 = 0; num187 < 255; num187++)
//				{
//					if (Main.player[num187].active)
//					{
//						Rectangle rectangle2 = new Rectangle((int)Main.player[num187].position.X - num186, (int)Main.player[num187].position.Y - num186, num186 * 2, num186 * 2);
//						if (rectangle.Intersects(rectangle2))
//						{
//							flag19 = false;
//							break;
//						}
//					}
//				}
//				if (flag19)
//				{
//					flag18 = true;
//				}
//			}
//			if (directional)
//			{
//				if (npc.velocity.X < 0f)
//				{
//					npc.spriteDirection = 1;
//				}
//				else if (npc.velocity.X > 0f)
//				{
//					npc.spriteDirection = -1;
//				}
//			}
//			float num188 = speed;
//			float num189 = turnSpeed;
//			Vector2 vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
//			float num191 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
//			float num192 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
//			num191 = (float)((int)(num191 / 16f) * 16);
//			num192 = (float)((int)(num192 / 16f) * 16);
//			vector18.X = (float)((int)(vector18.X / 16f) * 16);
//			vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
//			num191 -= vector18.X;
//			num192 -= vector18.Y;
//			float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
//			if (following > 0f && following < (float)Main.npc.Length)
//			{
//				try
//				{
//					vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
//					num191 = Main.npc[(int)following].position.X + (float)(Main.npc[(int)following].width / 2) - vector18.X;
//					num192 = Main.npc[(int)following].position.Y + (float)(Main.npc[(int)following].height / 2) - vector18.Y;
//				}
//				catch
//				{
//				}
//				npc.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
//				num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
//				int num194 = npc.width;
//				num193 = (num193 - (float)num194) / num193;
//				num191 *= num193;
//				num192 *= num193;
//				npc.velocity = Vector2.Zero;
//				npc.position.X = npc.position.X + num191;
//				npc.position.Y = npc.position.Y + num192;
//				if (directional)
//				{
//					if (num191 < 0f)
//					{
//						npc.spriteDirection = 1;
//					}
//					if (num191 > 0f)
//					{
//						npc.spriteDirection = -1;
//					}
//				}
//			}
//			else
//			{
//				if (!flag18)
//				{
//					npc.TargetClosest(true);
//					npc.velocity.Y = npc.velocity.Y + 0.11f;
//					if (npc.velocity.Y > num188)
//					{
//						npc.velocity.Y = num188;
//					}
//					if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.4)
//					{
//						if (npc.velocity.X < 0f)
//						{
//							npc.velocity.X = npc.velocity.X - num189 * 1.1f;
//						}
//						else
//						{
//							npc.velocity.X = npc.velocity.X + num189 * 1.1f;
//						}
//					}
//					else if (npc.velocity.Y == num188)
//					{
//						if (npc.velocity.X < num191)
//						{
//							npc.velocity.X = npc.velocity.X + num189;
//						}
//						else if (npc.velocity.X > num191)
//						{
//							npc.velocity.X = npc.velocity.X - num189;
//						}
//					}
//					else if (npc.velocity.Y > 4f)
//					{
//						if (npc.velocity.X < 0f)
//						{
//							npc.velocity.X = npc.velocity.X + num189 * 0.9f;
//						}
//						else
//						{
//							npc.velocity.X = npc.velocity.X - num189 * 0.9f;
//						}
//					}
//				}
//				else
//				{
//					if (!flies && npc.behindTiles && npc.soundDelay == 0)
//					{
//						float num195 = num193 / 40f;
//						if (num195 < 10f)
//						{
//							num195 = 10f;
//						}
//						if (num195 > 20f)
//						{
//							num195 = 20f;
//						}
//						npc.soundDelay = (int)num195;
//						Main.PlaySound(SoundID.Roar, npc.position, 1);
//					}
//					num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
//					float num196 = System.Math.Abs(num191);
//					float num197 = System.Math.Abs(num192);
//					float num198 = num188 / num193;
//					num191 *= num198;
//					num192 *= num198;
//					if (ShouldRun())
//					{
//						bool flag20 = true;
//						for (int num199 = 0; num199 < 255; num199++)
//						{
//							if (Main.player[num199].active && !Main.player[num199].dead && Main.player[num199].ZoneCorrupt)
//							{
//								flag20 = false;
//							}
//						}
//						if (flag20)
//						{
//							if (Main.netMode != NetmodeID.MultiplayerClient && (double)(npc.position.Y / 16f) > (Main.rockLayer + (double)Main.maxTilesY) / 2.0)
//							{
//								npc.active = false;
//								int num200 = (int)follower;
//								while (num200 > 0 && num200 < 200 && Main.npc[num200].active && Main.npc[num200].aiStyle == npc.aiStyle)
//								{
//									int num201 = (int)Main.npc[num200].ai[0];
//									Main.npc[num200].active = false;
//									npc.life = 0;
//									if (Main.netMode == NetmodeID.Server)
//									{
//										NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num200, 0f, 0f, 0f, 0, 0, 0);
//									}
//									num200 = num201;
//								}
//								if (Main.netMode == NetmodeID.Server)
//								{
//									NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
//								}
//							}
//							num191 = 0f;
//							num192 = num188;
//						}
//					}
//					bool flag21 = false;
//					if (npc.type == NPCID.WyvernHead)
//					{
//						if ((npc.velocity.X > 0f && num191 < 0f || npc.velocity.X < 0f && num191 > 0f || npc.velocity.Y > 0f && num192 < 0f || npc.velocity.Y < 0f && num192 > 0f) && System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y) > num189 / 2f && num193 < 300f)
//						{
//							flag21 = true;
//							if (System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y) < num188)
//							{
//								npc.velocity *= 1.1f;
//							}
//						}
//						if (npc.position.Y > Main.player[npc.target].position.Y || (double)(Main.player[npc.target].position.Y / 16f) > Main.worldSurface || Main.player[npc.target].dead)
//						{
//							flag21 = true;
//							if (System.Math.Abs(npc.velocity.X) < num188 / 2f)
//							{
//								if (npc.velocity.X == 0f)
//								{
//									npc.velocity.X = npc.velocity.X - (float)npc.direction;
//								}
//								npc.velocity.X = npc.velocity.X * 1.1f;
//							}
//							else
//							{
//								if (npc.velocity.Y > -num188)
//								{
//									npc.velocity.Y = npc.velocity.Y - num189;
//								}
//							}
//						}
//					}
//					if (!flag21)
//					{
//						if (npc.velocity.X > 0f && num191 > 0f || npc.velocity.X < 0f && num191 < 0f || npc.velocity.Y > 0f && num192 > 0f || npc.velocity.Y < 0f && num192 < 0f)
//						{
//							if (npc.velocity.X < num191)
//							{
//								npc.velocity.X = npc.velocity.X + num189;
//							}
//							else
//							{
//								if (npc.velocity.X > num191)
//								{
//									npc.velocity.X = npc.velocity.X - num189;
//								}
//							}
//							if (npc.velocity.Y < num192)
//							{
//								npc.velocity.Y = npc.velocity.Y + num189;
//							}
//							else
//							{
//								if (npc.velocity.Y > num192)
//								{
//									npc.velocity.Y = npc.velocity.Y - num189;
//								}
//							}
//							if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && (npc.velocity.X > 0f && num191 < 0f || npc.velocity.X < 0f && num191 > 0f))
//							{
//								if (npc.velocity.Y > 0f)
//								{
//									npc.velocity.Y = npc.velocity.Y + num189 * 2f;
//								}
//								else
//								{
//									npc.velocity.Y = npc.velocity.Y - num189 * 2f;
//								}
//							}
//							if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && (npc.velocity.Y > 0f && num192 < 0f || npc.velocity.Y < 0f && num192 > 0f))
//							{
//								if (npc.velocity.X > 0f)
//								{
//									npc.velocity.X = npc.velocity.X + num189 * 2f;
//								}
//								else
//								{
//									npc.velocity.X = npc.velocity.X - num189 * 2f;
//								}
//							}
//						}
//						else
//						{
//							if (num196 > num197)
//							{
//								if (npc.velocity.X < num191)
//								{
//									npc.velocity.X = npc.velocity.X + num189 * 1.1f;
//								}
//								else if (npc.velocity.X > num191)
//								{
//									npc.velocity.X = npc.velocity.X - num189 * 1.1f;
//								}
//								if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
//								{
//									if (npc.velocity.Y > 0f)
//									{
//										npc.velocity.Y = npc.velocity.Y + num189;
//									}
//									else
//									{
//										npc.velocity.Y = npc.velocity.Y - num189;
//									}
//								}
//							}
//							else
//							{
//								if (npc.velocity.Y < num192)
//								{
//									npc.velocity.Y = npc.velocity.Y + num189 * 1.1f;
//								}
//								else if (npc.velocity.Y > num192)
//								{
//									npc.velocity.Y = npc.velocity.Y - num189 * 1.1f;
//								}
//								if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
//								{
//									if (npc.velocity.X > 0f)
//									{
//										npc.velocity.X = npc.velocity.X + num189;
//									}
//									else
//									{
//										npc.velocity.X = npc.velocity.X - num189;
//									}
//								}
//							}
//						}
//					}
//				}
//				npc.rotation = (float)System.Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
//				if (isHead)
//				{
//					if (flag18)
//					{
//						if (npc.localAI[0] != 1f)
//						{
//							npc.netUpdate = true;
//						}
//						npc.localAI[0] = 1f;
//					}
//					else
//					{
//						if (npc.localAI[0] != 0f)
//						{
//							npc.netUpdate = true;
//						}
//						npc.localAI[0] = 0f;
//					}
//					if ((npc.velocity.X > 0f && npc.oldVelocity.X < 0f || npc.velocity.X < 0f && npc.oldVelocity.X > 0f || npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f || npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f) && !npc.justHit)
//					{
//						npc.netUpdate = true;
//						return;
//					}
//				}
//			}
//			CustomBehavior();
//		}

//		public virtual void Init()
//		{
//		}

//		public virtual bool ShouldRun()
//		{
//			return false;
//		}

//		public virtual void CustomBehavior()
//		{
//		}

//		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
//		{
//			return isHead ? (bool?)null : false;
//		}
//	}
//}