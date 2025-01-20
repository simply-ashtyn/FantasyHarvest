// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Actors/BaseCharacter.h"
#include "BasePlayer.generated.h"

/**
 * 
 */
UCLASS()
class FANTASYHARVEST_API ABasePlayer : public ABaseCharacter
{
	GENERATED_BODY()
	
public:
	ABasePlayer();

	UPROPERTY(BlueprintReadWrite, EditAnywhere)
	bool bInventoryOpen;
	//UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Achievement")
	//class UAchievementSystem* AchievementComponent;
	//UFUNCTION()
	//void AchievementUnlocked(FText AchievementName, UTexture2D* AchievementIcon);

protected:
	void BeginPlay() override;
	virtual void Tick(float DeltaTime) override;
	void SetupPlayerInputComponent(class UInputComponent* PlayerInputComponent) override;
	void HandleTakeDamage(float Ratio) override;
	void HandleDeath(float Ratio) override;

	UPROPERTY(VisibleAnywhere, BlueprintReadOnly)
	class USpringArmComponent* SpringArm;
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly)
	class UCameraComponent* PlayerCamera;

	bool bIsCasting;
	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly, Category = "Stamina")
	float MaxMana;
	UPROPERTY(BlueprintReadOnly, EditDefaultsOnly, Category = "Stamina")
	float CurrentMana;

	void RestoreStamina(float Time);

	//UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "UI")
	//TSubclassOf<UUserWidget> PauseMenuClass;
	//UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "UI")
	//TSubclassOf<class UPlayerHUD> PlayerHUDClass;
	//UPROPERTY()
	//class UPlayerHUD* PlayerHUD;

	/// INVENTORY

	//UPROPERTY(BlueprintReadWrite, EditAnywhere, Category = "Inventory")
	//TSubclassOf<UUserWidget> Invetoryclass;

private:
	APlayerController* PlayerController;
	void InputAxisMoveForward(float AxisValue);
	void Strafe(float value);
	void SprintStart();
	void SprintEnd();


	float WalkSpeed;
	float SprintSpeed;
};
