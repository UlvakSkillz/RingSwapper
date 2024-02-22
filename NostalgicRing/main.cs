using MelonLoader;
using RUMBLE.Combat.ShiftStones;
using System;
using UnityEngine;

namespace NostalgicRing
{
    public class main : MelonMod
	{
		//Variables
		private string settingsFile = @"UserData\RingSwapper\Settings.txt";
		private string currentScene = "";
		private bool sceneChanged = false;
		private int[] ringTypes = new int[3];
		private int ringType = 0;
		private bool gymInitRan = false;
		private GameObject materialsGameObject;
		private Material[] materials;
		System.Random random = new System.Random();
		//RumbleBee is so Cool!

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			currentScene = sceneName;
			sceneChanged = true;
			//Read Settings File
			if (System.IO.File.Exists(settingsFile))
			{
				try
				{
					string[] fileContents = System.IO.File.ReadAllLines(settingsFile);
					ringTypes[0] = Int32.Parse(fileContents[0]);
					ringTypes[1] = Int32.Parse(fileContents[1]);
					ringTypes[2] = Int32.Parse(fileContents[2]);
					//if random, set random
					if (fileContents[3].ToString().ToLower() == "true")
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
				}
				catch (Exception e)
				{
					MelonLogger.Error($"Error Reading {settingsFile}: {e}");
				}
			}
			else
			{
				MelonLogger.Error($"File not Found: {settingsFile}");
			}
		}

        public override void OnFixedUpdate()
        {
			if (sceneChanged)
            {
                try
				{
					if (currentScene == "Gym")
					{
						//initialize
						if (!gymInitRan)
						{
							materials = new Material[13];
							/*Suit*/materials[0] = new Material(GameObject.Find("Player Controller(Clone)/Visuals/Suit").GetComponent<SkinnedMeshRenderer>().material);
							/*TreeRoot*/materials[1] = new Material(GameObject.Find("--------------SCENE--------------/Gym_Production/Sub static group(buildings)/Rumble_station/Root").GetComponent<MeshRenderer>().material);
							/*SpawnRingFloor*/materials[2] = new Material(GameObject.Find("--------------SCENE--------------/Gym_Production/Main static group/Spawn_area/SpawnRingFloor").GetComponent<MeshRenderer>().material);
							/*Leaves*/materials[3] = new Material(GameObject.Find("--------------SCENE--------------/Gym_Production/Main static group/Foliage/Root_leaves").GetComponent<MeshRenderer>().material);
							/*Dirt*/materials[4] = new Material(GameObject.Find("--------------SCENE--------------/Gym_Production/Main static group/FloorMeshParent/Floormesh").GetComponent<MeshRenderer>().material);
							/*Wood*/materials[5] = new Material(GameObject.Find("--------------SCENE--------------/Gym_Production/Sub static group(buildings)/Rumble_station/Wood/Bench/Woodset_large__1__1").GetComponent<MeshRenderer>().material);
                            try
                            {
								/*ChargeStone*/materials[6] = new Material(GameObject.Find("Game Instance/Pre-Initializable/PoolManager/Pool: ChargeStone (RUMBLE.Combat.ShiftStones.ChargeStone)/ChargeStone").GetComponent<ChargeStone>().gemRenderer.material);
                            }
                            catch
							{
								/*ChargeStone Workaround*/
								PlayerShiftStoneSystem playerShiftStoneSystem = GameObject.Find("Player Controller(Clone)/Shiftstones").GetComponent<PlayerShiftStoneSystem>();
								materials[6] = new Material(playerShiftStoneSystem.GetEquippedShiftstones()[playerShiftStoneSystem.GetSocketIndex("Charge Stone")].gemRenderer.material);
							}
                            try
                            {
								/*AdamantStone*/materials[7] = new Material(GameObject.Find("Game Instance/Pre-Initializable/PoolManager/Pool: AdamantStone (RUMBLE.Combat.ShiftStones.UnyieldingStone)/AdamantStone").GetComponent<UnyieldingStone>().gemRenderer.material);
                            }
                            catch
							{
								/*AdamantStone Workaround*/
								PlayerShiftStoneSystem playerShiftStoneSystem = GameObject.Find("Player Controller(Clone)/Shiftstones").GetComponent<PlayerShiftStoneSystem>();
								materials[7] = new Material(playerShiftStoneSystem.GetEquippedShiftstones()[playerShiftStoneSystem.GetSocketIndex("Adamant Stone")].gemRenderer.material);
							}
                            try
                            {
								/*GuardStone*/materials[8] = new Material(GameObject.Find("Game Instance/Pre-Initializable/PoolManager/Pool: GuardStone (RUMBLE.Combat.ShiftStones.GuardStone)/GuardStone").GetComponent<GuardStone>().gemRenderer.material);
                            }
                            catch
							{
								/*GuardStone Workaround*/
								PlayerShiftStoneSystem playerShiftStoneSystem = GameObject.Find("Player Controller(Clone)/Shiftstones").GetComponent<PlayerShiftStoneSystem>();
								materials[8] = new Material(playerShiftStoneSystem.GetEquippedShiftstones()[playerShiftStoneSystem.GetSocketIndex("Guard Stone")].gemRenderer.material);
							}
							try
							{
								/*VolatileStone*/materials[9] = new Material(GameObject.Find("Game Instance/Pre-Initializable/PoolManager/Pool: VolatileStone (RUMBLE.Combat.ShiftStones.VolatileStone)/VolatileStone").GetComponent<VolatileStone>().gemRenderer.material);
							}
							catch
							{
								/*VolatileStone Workaround*/
								PlayerShiftStoneSystem playerShiftStoneSystem = GameObject.Find("Player Controller(Clone)/Shiftstones").GetComponent<PlayerShiftStoneSystem>();
								materials[9] = new Material(playerShiftStoneSystem.GetEquippedShiftstones()[playerShiftStoneSystem.GetSocketIndex("Volatile Stone")].gemRenderer.material);
							}
							try
							{
								/*SurgeStone*/materials[10] = new Material(GameObject.Find("Game Instance/Pre-Initializable/PoolManager/Pool: SurgeStone (RUMBLE.Combat.ShiftStones.CounterStone)/SurgeStone").GetComponent<CounterStone>().gemRenderer.material);
							}
							catch
							{
								/*SurgeStone Workaround*/
								PlayerShiftStoneSystem playerShiftStoneSystem = GameObject.Find("Player Controller(Clone)/Shiftstones").GetComponent<PlayerShiftStoneSystem>();
								materials[10] = new Material(playerShiftStoneSystem.GetEquippedShiftstones()[playerShiftStoneSystem.GetSocketIndex("Surge Stone")].gemRenderer.material);
							}
							/*LargeRock*/materials[11] = new Material(GameObject.Find("Game Instance/Pre-Initializable/PoolManager/Pool: LargeRock (RUMBLE.MoveSystem.Structure)/LargeRock").GetComponent<MeshRenderer>().material);
							try
							{
								/*FlowStone*/materials[12] = new Material(GameObject.Find("--------------LOGIC--------------/Heinhouser products/ShiftstoneCabinet/Cabinet/ShiftstoneBox/FlowStone/Gem10 (1)").GetComponent<MeshRenderer>().material);
							}
							catch
							{
								/*FlowStone Workaround*/
								PlayerShiftStoneSystem playerShiftStoneSystem = GameObject.Find("Player Controller(Clone)/Shiftstones").GetComponent<PlayerShiftStoneSystem>();
								materials[11] = new Material(playerShiftStoneSystem.GetEquippedShiftstones()[playerShiftStoneSystem.GetSocketIndex("Flow Stone")].gemRenderer.material);
							}
							materialsGameObject = new GameObject();
							materialsGameObject.AddComponent<MeshRenderer>();
							materialsGameObject.GetComponent<MeshRenderer>().materials = materials;
							GameObject.DontDestroyOnLoad(materialsGameObject);
							gymInitRan = true;
							MelonLogger.Msg("Initialized");
						}
					}
					//switch rings on load
					else if (currentScene == "Map0")
					{
						SwitchRingType(GameObject.Find("Map0_production/Main static group/Ring_boarder"));
					}
					else if (currentScene == "Map1")
					{
						SwitchRingType(GameObject.Find("Map1_production/Main static group/RingClamp"));
					}
					else if (currentScene == "Park")
					{
						SwitchRingType(GameObject.Find("________________SCENE_________________/Park/Main static group/Arenas/Gymarena/Arena"));
					}
					sceneChanged = false;
				}
                catch
                {
					return;
                }
            }
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
				switch (ringType)
				{
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
