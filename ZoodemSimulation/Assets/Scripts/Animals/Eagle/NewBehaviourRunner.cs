using System;
//using System.Collections.Generic;
//using UnityEngine;
//using BehaviourAPI.Core;
//using BehaviourAPI.Core.Actions;
//using BehaviourAPI.Core.Perceptions;
//using BehaviourAPI.UnityToolkit;
//using BehaviourAPI.BehaviourTrees;
//
//public class NewBehaviourRunner : BehaviourRunner
//{
//	public Eagle eagle;
//	
//	protected override BehaviourGraph CreateGraph()
//	{
//		var perceptionisInBiome = new ConditionPerception(()=>eagle.isInBiome());
//        BehaviourTree CrearNidoAguila = new BehaviourTree();
//		
//		SimpleAction CrearNido_action = new SimpleAction(eagle.createNest);
//		LeafNode CrearNido = CrearNidoAguila.CreateLeafNode(CrearNido_action);
//		
//		ConditionNode CercaDeNido = CrearNidoAguila.CreateDecorator<ConditionNode>(CrearNido);
//		
//		SimpleAction Deambular_action = new SimpleAction(eagle.walk);
//		LeafNode Deambular = CrearNidoAguila.CreateLeafNode(Deambular_action);
//		
//		SelectorNode unnamed_2 = CrearNidoAguila.CreateComposite<SelectorNode>(false, CercaDeNido, Deambular);
//		unnamed_2.IsRandomized = false;
//		
//		ConditionNode EnBiomaDePreferencia = CrearNidoAguila.CreateDecorator<ConditionNode>(unnamed_2).SetPerception(perceptionisInBiome);
//		
//		SimpleAction ViajarBiomaPreferencia_action = new SimpleAction(eagle.travelBiome);
//		LeafNode ViajarBiomaPreferencia = CrearNidoAguila.CreateLeafNode(ViajarBiomaPreferencia_action);
//		
//		SelectorNode unnamed_1 = CrearNidoAguila.CreateComposite<SelectorNode>(false, EnBiomaDePreferencia, ViajarBiomaPreferencia);
//		unnamed_1.IsRandomized = false;
//		
//		LoopNode unnamed = CrearNidoAguila.CreateDecorator<LoopNode>(unnamed_1);
//		unnamed.Iterations = -1;
//		
//		return CrearNidoAguila;
//	}
//}
