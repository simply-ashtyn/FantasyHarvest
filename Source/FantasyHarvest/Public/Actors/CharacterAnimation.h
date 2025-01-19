// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Animation/AnimInstance.h"
#include "CharacterAnimation.generated.h"

/**
 * 
 */
UCLASS()
class FANTASYHARVEST_API UCharacterAnimation : public UAnimInstance
{
	GENERATED_BODY()
	
public:
	UCharacterAnimation();
	virtual void NativeUpdateAnimation(float DeltaSeconds) override;
	FName SlotNodeName;

	/// ANIMATIONS
	UFUNCTION(BlueprintCallable, BlueprintNativeEvent)
	void PlayHurtAnimation(float percent);
	virtual void PlayHurtAnimation_Implementation(float percent);

	UFUNCTION(BlueprintCallable, BlueprintNativeEvent)
	void PlayDeathAnimation(float percent);
	virtual void PlayDeathAnimation_Implementation(float percent);

protected:
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly)
	float Velocity;
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly)
	float Direction;

	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly, Category = Animation)
	class UAnimSequenceBase* HurtAsset;
	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly, Category = Animation)
	class UAnimSequenceBase* DeathAsset;

	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly)
	bool DebugHurt;
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly)
	bool DebugDeath;

	UFUNCTION(BlueprintCallable, BlueprintNativeEvent)
	void PreviewWindowUpdate();
	virtual void PreviewWindowUpdate_Implementation();
};
