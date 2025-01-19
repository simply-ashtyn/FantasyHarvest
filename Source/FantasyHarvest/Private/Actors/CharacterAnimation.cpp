// Fill out your copyright notice in the Description page of Project Settings.


#include "Actors/CharacterAnimation.h"

UCharacterAnimation::UCharacterAnimation()
{
	SlotNodeName = "Action";
	Velocity = 0;
	Direction = 0;
}

void UCharacterAnimation::NativeUpdateAnimation(float DeltaSeconds)
{
	Super::NativeUpdateAnimation(DeltaSeconds);

	APawn* Pawn = TryGetPawnOwner();
	if (Pawn)
	{
		Velocity = Pawn->GetVelocity().Size();
		Direction = CalculateDirection(Pawn->GetVelocity(), Pawn->GetActorRotation());
	}
	else
	{
		PreviewWindowUpdate();
	}
}

void UCharacterAnimation::PlayHurtAnimation_Implementation(float percent)
{
	PlaySlotAnimationAsDynamicMontage(HurtAsset, SlotNodeName);
}

void UCharacterAnimation::PlayDeathAnimation_Implementation(float percent)
{
	PlaySlotAnimationAsDynamicMontage(DeathAsset, SlotNodeName);
}

void UCharacterAnimation::PreviewWindowUpdate_Implementation()
{
	if (DebugHurt)
	{
		PlayHurtAnimation(0);
		DebugHurt = false;
	}
	else if (DebugDeath)
	{
		PlayDeathAnimation(0);
		DebugDeath = false;
	}
}
