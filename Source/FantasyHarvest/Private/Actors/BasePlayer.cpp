// Fill out your copyright notice in the Description page of Project Settings.


#include "Actors/BasePlayer.h"
#include "GameFramework/SpringArmComponent.h"
#include "Camera/CameraComponent.h"
#include "GameFramework/CharacterMovementComponent.h"

ABasePlayer::ABasePlayer()
{
	PrimaryActorTick.bCanEverTick = true;

	SpringArm = CreateDefaultSubobject<USpringArmComponent>(TEXT("SpringArm"));
	SpringArm->SetRelativeLocation(FVector{ 0.f, 80.f, 90.f });
	SpringArm->TargetArmLength = 205.0f;
	SpringArm->SetupAttachment(GetRootComponent());
	SpringArm->bUsePawnControlRotation = true;

	PlayerCamera = CreateDefaultSubobject<UCameraComponent>(TEXT("PlayerCamera"));
	PlayerCamera->SetupAttachment(SpringArm);

	//AchievementComponent = CreateDefaultSubobject<UAchievementSystem>(TEXT("AchievementComponent"));

	bInventoryOpen = false;
	bIsCasting = false;
	WalkSpeed = 600.f;
	SprintSpeed = 1200.f;
	MaxMana = 50.f;
}

void ABasePlayer::BeginPlay()
{
	Super::BeginPlay();
	CurrentMana = MaxMana;
	PlayerController = (APlayerController*)GetController();
}

void ABasePlayer::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	RestoreStamina(DeltaTime);
}

void ABasePlayer::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);

	PlayerInputComponent->BindAxis("TurnRight", this, &ABasePlayer::AddControllerYawInput);
	PlayerInputComponent->BindAxis("LookUp", this, &ABasePlayer::AddControllerPitchInput);
	PlayerInputComponent->BindAxis("MoveForward", this, &ABasePlayer::InputAxisMoveForward);
	PlayerInputComponent->BindAxis("Strafe", this, &ABasePlayer::Strafe);
	PlayerInputComponent->BindAction("Sprint", IE_Pressed, this, &ABasePlayer::SprintStart);
	PlayerInputComponent->BindAction("Sprint", IE_Released, this, &ABasePlayer::SprintEnd);
	//PlayerInputComponent->BindAction("Inventory", IE_Pressed, this, &ABasePlayer::InventoryMenu);
}

void ABasePlayer::InputAxisMoveForward(float AxisValue)
{
	AddMovementInput(FRotator(0.f, GetControlRotation().Yaw, 0.f).Vector(), AxisValue);
}

void ABasePlayer::Strafe(float value)
{
	FRotator MakeRotation = FRotator(0.f, GetControlRotation().Yaw, 0.f);
	AddMovementInput(FRotationMatrix(MakeRotation).GetScaledAxis(EAxis::Y), value);
}

void ABasePlayer::SprintStart()
{
	GetCharacterMovement()->MaxWalkSpeed = SprintSpeed;
}

void ABasePlayer::SprintEnd()
{
	GetCharacterMovement()->MaxWalkSpeed = WalkSpeed;
}

void ABasePlayer::HandleTakeDamage(float Ratio)
{

}

void ABasePlayer::HandleDeath(float Ratio)
{

}

void ABasePlayer::RestoreStamina(float Time)
{
	if (CurrentMana < MaxMana)
	{
		CurrentMana++;
	}
}