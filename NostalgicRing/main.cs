using MelonLoader;
using Il2CppRUMBLE.Combat.ShiftStones;
using RumbleModUI;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using RumbleModdingAPI;
using Il2CppRUMBLE.Managers;
using Il2CppRUMBLE.Combat.ShiftStones.UI;
using static RumbleModdingAPI.Calls.GameObjects.Gym.Logic.HeinhouserProducts.ShiftstoneCabinet;

namespace NostalgicRing
{
    public class main : MelonMod
	{
		//Variables
		private string currentScene = "Loader";
		private bool sceneChanged = false;
		private int[] ringTypes = new int[3];
		private int ringType = 0;
		private bool gymInitRan = false;
		private GameObject materialsGameObject;
		private Material[] materials;
		System.Random random = new System.Random();
		UI UI = UI.instance;
		private Mod RingSwapper = new Mod();
		private Material[] originalRing;
		GameObject originalRingGameObject;

		public override void OnLateInitializeMelon()
		{
			RingSwapper.ModName = "Ring Swapper";
			RingSwapper.ModVersion = "2.2.1";
			RingSwapper.SetFolder("RingSwapper");
			RingSwapper.AddDescription("Description", "Description", "Changes the Rings in each Map", new Tags { IsSummary = true });
			RingSwapper.AddToList("Park", 14, "0 = Default            | 1 = Dusty Metal" + Environment.NewLine + "2 = Deep Metal     | 3 = Bright Metal" + Environment.NewLine + "4 = Yin and Yang  | 5 = Rustic" + Environment.NewLine + "6 = Plant Life         | 7 = Wooden" + Environment.NewLine + "8 = RumbleBee     | 9 = Rumblekai" + Environment.NewLine + "10 = UlvakSkillz    | 11 = Honey" + Environment.NewLine + "12 = Christmas     | 13 = Rocky" + Environment.NewLine + "14 = Spaceship     | 15 = Candy", new Tags { });
			RingSwapper.AddToList("Ring", 7, "0 = Default            | 1 = Dusty Metal" + Environment.NewLine + "2 = Deep Metal     | 3 = Bright Metal" + Environment.NewLine + "4 = Yin and Yang  | 5 = Rustic" + Environment.NewLine + "6 = Plant Life         | 7 = Wooden" + Environment.NewLine + "8 = RumbleBee     | 9 = Rumblekai" + Environment.NewLine + "10 = UlvakSkillz    | 11 = Honey" + Environment.NewLine + "12 = Christmas     | 13 = Rocky" + Environment.NewLine + "14 = Spaceship     | 15 = Candy", new Tags { });
			RingSwapper.AddToList("Pit", 3, "0 = Default            | 1 = Dusty Metal" + Environment.NewLine + "2 = Deep Metal     | 3 = Bright Metal" + Environment.NewLine + "4 = Yin and Yang  | 5 = Rustic" + Environment.NewLine + "6 = Plant Life         | 7 = Wooden" + Environment.NewLine + "8 = RumbleBee     | 9 = Rumblekai" + Environment.NewLine + "10 = UlvakSkillz    | 11 = Honey" + Environment.NewLine + "12 = Christmas     | 13 = Rocky" + Environment.NewLine + "14 = Spaceship     | 15 = Candy", new Tags { });
			RingSwapper.AddToList("Random", false, 0, "Changes Rings to Random Types", new Tags { });
			RingSwapper.GetFromFile();
			ringTypes[0] = (int)RingSwapper.Settings[1].Value;
			ringTypes[1] = (int)RingSwapper.Settings[2].Value;
			ringTypes[2] = (int)RingSwapper.Settings[3].Value;
			RingSwapper.ModSaved += Save;
			UI.instance.UI_Initialized += UIInit;
			Calls.onMapInitialized += MapChange;
		}

		public void UIInit()
		{
			UI.AddMod(RingSwapper);
		}

		public void Save()
		{
			ringTypes[0] = (int)RingSwapper.Settings[1].Value;
			ringTypes[1] = (int)RingSwapper.Settings[2].Value;
			ringTypes[2] = (int)RingSwapper.Settings[3].Value;
			if (currentScene == "Map0")
			{
				SwitchRingType(Calls.GameObjects.Map0.Map0Production.MainStaticGroup.RingBoarder.GetGameObject());
			}
			else if (currentScene == "Map1")
			{
				SwitchRingType(Calls.GameObjects.Map1.Map1Production.MainStaticGroup.RingClamp.GetGameObject());
			}
			else if (currentScene == "Park")
			{
				SwitchRingType(Calls.GameObjects.Park.Scene.Park.MainStaticGroup.Arenas.GymArena0.Arena.GetGameObject());
			}
		}

		private void MapChange()
		{
			try
			{
				//Read Settings File
				if (currentScene != "Loader")
				{
					ringTypes[0] = (int)RingSwapper.Settings[1].Value;
					ringTypes[1] = (int)RingSwapper.Settings[2].Value;
					ringTypes[2] = (int)RingSwapper.Settings[3].Value;
					//if random, set random
					if ((bool)RingSwapper.Settings[4].Value)
					{
						ringTypes[0] = random.Next(0, 15);
						ringTypes[1] = random.Next(0, 15);
						ringTypes[2] = random.Next(0, 15);
					}
					//Clamp to 0 - 15
					if ((ringType < 0) || (15 < ringType))
					{
						ringType = 0;
					}
					MelonCoroutines.Start(WaitThenSwap());
				}
			}
			catch { }
		}

		private IEnumerator WaitThenSwap()
		{
			//switch rings on load
			if (currentScene == "Map0")
			{
				originalRing = new Material[Calls.GameObjects.Map0.Map0Production.MainStaticGroup.RingBoarder.GetGameObject().GetComponent<MeshRenderer>().materials.Count];
				for (int i = 0; i < Calls.GameObjects.Map0.Map0Production.MainStaticGroup.RingBoarder.GetGameObject().GetComponent<MeshRenderer>().materials.Count; i++)
				{
					originalRing[i] = new Material(Calls.GameObjects.Map0.Map0Production.MainStaticGroup.RingBoarder.GetGameObject().GetComponent<MeshRenderer>().materials[i]);
				}
				SwitchRingType(Calls.GameObjects.Map0.Map0Production.MainStaticGroup.RingBoarder.GetGameObject());
			}
			else if (currentScene == "Map1")
			{
				originalRing = new Material[Calls.GameObjects.Map1.Map1Production.MainStaticGroup.RingClamp.GetGameObject().GetComponent<MeshRenderer>().materials.Count];
				for (int i = 0; i < Calls.GameObjects.Map1.Map1Production.MainStaticGroup.RingClamp.GetGameObject().GetComponent<MeshRenderer>().materials.Count; i++)
				{
					originalRing[i] = new Material(Calls.GameObjects.Map1.Map1Production.MainStaticGroup.RingClamp.GetGameObject().GetComponent<MeshRenderer>().materials[i]);
				}
				SwitchRingType(Calls.GameObjects.Map1.Map1Production.MainStaticGroup.RingClamp.GetGameObject());
			}
			else if (currentScene == "Park")
			{
				yield return new WaitForFixedUpdate();
				yield return new WaitForFixedUpdate();
				SwitchRingType(Calls.GameObjects.Park.Scene.Park.MainStaticGroup.Arenas.GymArena0.Arena.GetGameObject());
			}
			yield break;
		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			currentScene = sceneName;
			sceneChanged = true;
		}

        public override void OnFixedUpdate()
        {
			if (sceneChanged)
			{
				if (currentScene == "Gym")
				{
					//initialize
					if (!gymInitRan) { MelonCoroutines.Start(Init()); }
				}
				else if (currentScene == "Park")
				{
					GameObject parkRing = GameObject.Find("________________SCENE_________________/Park/Main static group/Arenas/Gymarena/Arena");
					originalRing = new Material[parkRing.GetComponent<MeshRenderer>().materials.Count];
					for (int i = 0; i < parkRing.GetComponent<MeshRenderer>().materials.Count; i++)
					{
						originalRing[i] = new Material(parkRing.GetComponent<MeshRenderer>().materials[i]);
					}
					originalRingGameObject = new GameObject();
					(originalRingGameObject.AddComponent<MeshRenderer>()).materials = originalRing;
					originalRingGameObject.SetActive(false);
				}
				sceneChanged = false;
			}
        }

        private IEnumerator Init()
		{
			while (!gymInitRan)
			{
				yield return new WaitForFixedUpdate();
				try
				{
					materials = new Material[13];
                    /*Suit*/
                    materials[0] = new Material(PlayerManager.instance.localPlayer.Controller.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material);
                    /*TreeRoot*/
                    materials[1] = new Material(Calls.GameObjects.Gym.Scene.GymProduction.SubStaticGroupBuildings.RumbleStation.Root.GetGameObject().GetComponent<MeshRenderer>().material);
                    /*SpawnRingFloor*/
                    materials[2] = new Material(Calls.GameObjects.Gym.Scene.GymProduction.MainStaticGroup.SpawnArea.SpawnRingFloor.GetGameObject().GetComponent<MeshRenderer>().material);
                    /*Leaves*/
                    materials[3] = new Material(Calls.GameObjects.Gym.Scene.GymProduction.MainStaticGroup.Foliage.RootLeaves000.GetGameObject().GetComponent<MeshRenderer>().material);
                    /*Dirt*/
                    materials[4] = new Material(Calls.GameObjects.Gym.Scene.GymProduction.MainStaticGroup.FloorMeshParent.FloorMesh.GetGameObject().GetComponent<MeshRenderer>().material);
                    /*Wood*/
                    materials[5] = new Material(Calls.GameObjects.Gym.Scene.GymProduction.SubStaticGroupBuildings.RumbleStation.Wood.Bench.WoodsetLarge.GetGameObject().GetComponent<MeshRenderer>().material);
                    /*ChargeStone*/
                    materials[6] = new Material(Cabinet.ShiftstoneBox01.ChargeStone.GetGameObject().GetComponent<ChargeStone>().gemRenderer.material);
                    /*AdamantStone*/
                    materials[7] = new Material(Cabinet.ShiftstoneBox00.AdamantStone.GetGameObject().GetComponent<UnyieldingStone>().gemRenderer.material);
                    /*GuardStone*/
                    materials[8] = new Material(Cabinet.ShiftstoneBox03.GuardStone.GetGameObject().GetComponent<GuardStone>().gemRenderer.material);
                    /*VolatileStone*/
                    materials[9] = new Material(Cabinet.ShiftstoneBox07.VolatileStone.GetGameObject().GetComponent<VolatileStone>().gemRenderer.material);
                    /*SurgeStone*/
                    materials[10] = new Material(Cabinet.ShiftstoneBox05.SurgeStone.GetGameObject().GetComponent<CounterStone>().gemRenderer.material);
                    /*Rocks*/
                    materials[11] = new Material(Calls.GameObjects.Gym.Scene.GymProduction.SubStaticGroup.Rocks.FarGym.FarRocks0.GetGameObject().GetComponent<MeshRenderer>().material);
                    /*FlowStone*/
                    materials[12] = new Material(Cabinet.ShiftstoneBox02.FlowStone.GetGameObject().GetComponent<FlowStone>().gemRenderer.material);

                    materialsGameObject = new GameObject();
					materialsGameObject.name = "RingSwapper Materials";
					materialsGameObject.AddComponent<MeshRenderer>();
					materialsGameObject.GetComponent<MeshRenderer>().materials = materials;
					GameObject.DontDestroyOnLoad(materialsGameObject);
					gymInitRan = true;
					Log("Initialized");
				}
				catch (Exception e){ MelonLogger.Error(e); }
			}
			yield break;
		}

		private void SwitchRingType(GameObject objectToModify)
		{
			try
			{
				//set ringType dependent on Map
				switch (currentScene)
				{
					case "Park":
						ringType = ringTypes[0];
						break;
					case "Map0":
						ringType = ringTypes[1];
						break;
					case "Map1":
						ringType = ringTypes[2];
						break;
                }
				//reskin dependent on ringType
                objectToModify.GetComponent<MeshRenderer>().materials = originalRing;
                switch (ringType)
				{
					case 0:
						break;
					case 1: //Dusty Metal
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[1].shader, materialsGameObject.GetComponent<MeshRenderer>().materials[1].shader);
						break;
					case 2: //Deep Metal
						CopyReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[1], materialsGameObject.GetComponent<MeshRenderer>().materials[1]);
						break;
					case 3: //Bright Metal
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[0].shader, materialsGameObject.GetComponent<MeshRenderer>().materials[0].shader);
						break;
					case 4: //Yin and Yang
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[2].shader, materialsGameObject.GetComponent<MeshRenderer>().materials[0].shader);
						break;
					case 5: //Rustic
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[1], materialsGameObject.GetComponent<MeshRenderer>().materials[1]);
						break;
					case 6: //Plant Life
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[4], materialsGameObject.GetComponent<MeshRenderer>().materials[3]);
						break;
					case 7: //Wooden
                        ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[5], materialsGameObject.GetComponent<MeshRenderer>().materials[5]);
						break;
					case 8: //RumbleBee
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[2].shader, materialsGameObject.GetComponent<MeshRenderer>().materials[6]);
						break;
					case 9: //Rumblekai
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[6], materialsGameObject.GetComponent<MeshRenderer>().materials[2].shader);
						break;
					case 10: //UlvakSkillz
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[7], materialsGameObject.GetComponent<MeshRenderer>().materials[0].shader);
						break;
					case 11: //Honey
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[8], materialsGameObject.GetComponent<MeshRenderer>().materials[6]);
						break;
					case 12: //Christmas
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[9], materialsGameObject.GetComponent<MeshRenderer>().materials[10]);
						break;
					case 13: //Rocky
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[11], materialsGameObject.GetComponent<MeshRenderer>().materials[11]);
						break;
					case 14: //Spaceship
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[12], materialsGameObject.GetComponent<MeshRenderer>().materials[8]);
						break;
					case 15: //Candy
						ReskinRing(objectToModify.GetComponent<MeshRenderer>(), materialsGameObject.GetComponent<MeshRenderer>().materials[9], materialsGameObject.GetComponent<MeshRenderer>().materials[12]);
						break;
                }
            }
			catch (Exception e)
			{
				MelonLogger.Error(e);
			}
		}

		public static void Log(string msg)
		{
			MelonLogger.Msg(msg);
		}

		private void CopyReskinRing(MeshRenderer meshRendererToBeReskinned, Material mat1, Material mat2)
		{
			if (currentScene == "Map0")
			{
				meshRendererToBeReskinned.materials[0].CopyPropertiesFromMaterial(mat1);
				meshRendererToBeReskinned.materials[1].CopyPropertiesFromMaterial(mat2);
			}
			else if (currentScene == "Map1")
			{
				meshRendererToBeReskinned.materials[0].CopyPropertiesFromMaterial(mat2);
				meshRendererToBeReskinned.materials[1].CopyPropertiesFromMaterial(mat1);
			}
			else if (currentScene == "Park")
			{
				meshRendererToBeReskinned.materials[1].CopyPropertiesFromMaterial(mat1);
				meshRendererToBeReskinned.materials[2].CopyPropertiesFromMaterial(mat2);
			}
		}

		private void ReskinRing(MeshRenderer meshRendererToBeReskinned, Shader shader, Material material)
		{
			if (currentScene == "Map0")
			{
				meshRendererToBeReskinned.materials[0].shader = shader;
				Material[] tempMaterials = new Material[2];
				tempMaterials[0] = meshRendererToBeReskinned.materials[0];
				tempMaterials[1] = material;
				meshRendererToBeReskinned.materials = tempMaterials;
			}
			else if (currentScene == "Map1")
			{
				meshRendererToBeReskinned.materials[0].shader = shader;
				Material[] tempMaterials = new Material[2];
				tempMaterials[1] = meshRendererToBeReskinned.materials[0];
				tempMaterials[0] = material;
				meshRendererToBeReskinned.materials = tempMaterials;
			}
			else if (currentScene == "Park")
			{
				meshRendererToBeReskinned.materials[1].shader = shader;
				Material[] tempMaterials = new Material[3];
				tempMaterials[0] = meshRendererToBeReskinned.materials[0];
				tempMaterials[1] = meshRendererToBeReskinned.materials[1];
				tempMaterials[2] = material;
				meshRendererToBeReskinned.materials = tempMaterials;
			}
		}

		private void ReskinRing(MeshRenderer meshRendererToBeReskinned, Material material, Shader shader)
		{
			if (currentScene == "Map0")
			{
				Material[] tempMaterials = new Material[2];
				tempMaterials[0] = material;
				tempMaterials[1] = meshRendererToBeReskinned.materials[1];
				tempMaterials[1].shader = shader;
				meshRendererToBeReskinned.materials = tempMaterials;
			}
			else if (currentScene == "Map1")
			{
				Material[] tempMaterials = new Material[2];
				tempMaterials[1] = material;
				tempMaterials[0] = meshRendererToBeReskinned.materials[1];
				tempMaterials[0].shader = shader;
				meshRendererToBeReskinned.materials = tempMaterials;
			}
			else if (currentScene == "Park")
			{
				Material[] tempMaterials = new Material[3];
				tempMaterials[0] = meshRendererToBeReskinned.materials[0];
				tempMaterials[1] = material;
				tempMaterials[2] = meshRendererToBeReskinned.materials[2];
				tempMaterials[2].shader = shader;
				meshRendererToBeReskinned.materials = tempMaterials;
			}
		}

		private void ReskinRing(MeshRenderer meshRendererToBeReskinned, Shader shader1, Shader shader2)
		{
			if (currentScene == "Map0")
			{
				meshRendererToBeReskinned.materials[0].shader = shader1;
				meshRendererToBeReskinned.materials[1].shader = shader2;
			}
			else if (currentScene == "Map1")
			{
				meshRendererToBeReskinned.materials[1].shader = shader1;
				meshRendererToBeReskinned.materials[0].shader = shader2;
			}
			else if (currentScene == "Park")
			{
				meshRendererToBeReskinned.materials[1].shader = shader1;
				meshRendererToBeReskinned.materials[2].shader = shader2;
			}
		}

		private void ReskinRing(MeshRenderer meshRendererToBeReskinned, Material mat1, Material mat2)
		{
			if (currentScene == "Map0")
			{
				Material[] mats = new Material[2];
				mats[0] = mat1;
				mats[1] = mat2;
				meshRendererToBeReskinned.materials = mats;
			}
			else if (currentScene == "Map1")
			{
				Material[] mats = new Material[2];
				mats[1] = mat1;
				mats[0] = mat2;
				meshRendererToBeReskinned.materials = mats;
			}
			else if (currentScene == "Park")
			{
				Material[] mats = new Material[3];
				mats[0] = meshRendererToBeReskinned.materials[0];
				mats[1] = mat1;
				mats[2] = mat2;
				meshRendererToBeReskinned.materials = mats;
			}
		}
	}
}
