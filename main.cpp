#include "raylib.h"
#include "cmath"
#include <iostream>
using namespace std;

int CirsDown = 0;
float WindowoffsetX = 25.0f;
float WindowoffsetY = 225.0f;

// Struct for main object of the engine
struct Circle
{
    float PosX;
    float PosY;
        
    float VelX;
    float VelY;

    float ForceX;
    float ForceY;

    float radius;
    float mass;
    float elasticity;

    bool down;

    unsigned char red;
    unsigned char green;
    unsigned char blue;
};

/*bool CheckCollisionBoundingBox(float FPosX, float FPosY, float SPosX, float SPosY, float Frad, float Srad)
{
    if (FPosX + Frad + Srad > SPosX && FPosX < SPosX + Frad + Srad)
    {
        if (FPosY + Frad + Srad > SPosY && FPosY < SPosY + Frad + Srad)
        {
            return true;
        }
        else
        {
            return false;
        };
    }
    else
    {
        return false;
    };
};*/

bool CheckCollisionParticles(float FPosX, float FPosY, float SPosX, float SPosY, float Frad, float Srad)
{
    if ((FPosX - SPosX) * (FPosX - SPosX) + (FPosY - SPosY) * (FPosY - SPosY) <= (Srad + Frad) * (Srad + Frad))
    {
        return true;
    }
    else
    {
        return false;
    };
};

void UpdateParticles(Circle *circles, int NumOfCircles)
{
    float gravity = 0.5f;
    float TerminalVel = 10.0f;
    for (int i = 0; i < NumOfCircles; i++)
    {
        for (int j = 0; j < NumOfCircles; j++)
        {
            if (i != j)
            {
                if (CheckCollisionParticles(circles[i].PosX, circles[i].PosY, circles[j].PosX, circles[j].PosY, circles[i].radius, circles[j].radius))
                {
                    float DisX = circles[i].PosX - circles[j].PosX;
                    float DisY = circles[i].PosY - circles[j].PosY;

                    /*float Viscosity = 0.0002f;

                    if (Viscosity >= 1.0f)
                    {
                        Viscosity = 0.99999f;
                    }*/
                    

                    float length = sqrt((DisX * DisX) + (DisY * DisY));
                    float depth = length - (circles[i].radius + circles[j].radius + 1);

                    float UnitX = DisX / length;
                    float UnitY = DisY / length;

                    //circles[i].VelX *= (1 - Viscosity);
                    //circles[i].VelY *= (1 - Viscosity);
                    //circles[j].VelX *= (1 - Viscosity);
                    //circles[j].VelY *= (1 - Viscosity);

                    circles[i].VelX *= circles[i].elasticity * circles[j].elasticity;
                    circles[i].VelY *= circles[i].elasticity * circles[j].elasticity;
                    circles[j].VelX *= circles[i].elasticity * circles[j].elasticity;
                    circles[j].VelY *= circles[i].elasticity * circles[j].elasticity;

                    circles[i].ForceX -= UnitX * depth / 2;
                    circles[i].ForceY -= UnitY * depth / 2;

                    circles[j].ForceX += UnitX * depth / 2;
                    circles[j].ForceY += UnitY * depth / 2;
                };
            };   
        };

        circles[i].ForceY += gravity * circles[i].mass;

        if (circles[i].PosY >= 700 - circles[i].radius)
        {
            circles[i].PosY = 700 - circles[i].radius;
            circles[i].VelY *= circles[i].elasticity * -1;
        };
        if (circles[i].PosX <= 0 + circles[i].radius)
        {
            circles[i].PosX = circles[i].radius;
            circles[i].VelX *= circles[i].elasticity * -1;
        }else if (circles[i].PosX >= 1000 - circles[i].radius)
        {
            circles[i].PosX = 1000 - circles[i].radius;
            circles[i].VelX *= circles[i].elasticity * -1;
        };

        circles[i].VelX += (circles[i].ForceX / circles[i].mass) * GetFrameTime() * 60;
        circles[i].VelY += (circles[i].ForceY / circles[i].mass) * GetFrameTime() * 60;

        if (circles[i].VelY >= TerminalVel)
        {
            circles[i].VelY = TerminalVel;
        }else if (circles[i].VelY <= TerminalVel * -1)
        {
            circles[i].VelY = TerminalVel * -1;
        };

        if (circles[i].VelX >= TerminalVel)
        {
            circles[i].VelX = TerminalVel;
        }else if (circles[i].VelX <= TerminalVel * -1)
        {
            circles[i].VelX = TerminalVel * -1;
        };

        circles[i].PosX += circles[i].VelX * GetFrameTime() * 60;
        circles[i].PosY += circles[i].VelY * GetFrameTime() * 60;

        if (circles[i].down == false && circles[i].PosY > 0)
        {
            CirsDown++;
            circles[i].down = true;
        };
        
        circles[i].ForceX = 0;
        circles[i].ForceY = 0;
    };   
};

void DrawParticles(Circle *circles, int NumOfCircles)
{
    for (int i = 0; i < NumOfCircles; i++)
    {
        DrawCircleV(Vector2{circles[i].PosX + WindowoffsetX, circles[i].PosY + WindowoffsetY}, circles[i].radius, (Color){circles[i].red, circles[i].green, circles[i].blue, 255});
    };
};

int main()
{    
    InitWindow(1050, 950, "Physics Simulation");
    
    int NumOfCircles = 20;
    
    Circle Particles[NumOfCircles];

    for (int i = 0; i < NumOfCircles; i++)
    {
        Particles[i].VelX = 0.0f;
        Particles[i].VelY = 0;

        Particles[i].PosX = GetRandomValue(50 + i, 950 - i);
        Particles[i].PosY = -60 * i;

        Particles[i].radius = 20;
        Particles[i].mass = Particles[i].radius * 0.5f;
        Particles[i].elasticity = 0.5;

        Particles[i].red = 200;
        Particles[i].green = 200;
        Particles[i].blue = 200;

        Particles[i].down = false;
    };
    
    SetTargetFPS(60);
    while (!WindowShouldClose())
    {
        UpdateParticles(Particles, NumOfCircles);

        BeginDrawing();
            ClearBackground((Color){25, 25, 25, 255});

            DrawText(TextFormat("%i", GetFPS()), 40, 20, 20, (Color){200, 200, 200, 255});
            DrawText(TextFormat("%i", CirsDown), 600, 20, 20, (Color){200, 200, 200, 255});

            DrawLineV(Vector2{0 + WindowoffsetX, 700 + WindowoffsetY}, Vector2{1000 + WindowoffsetX, 700 + WindowoffsetY}, (Color){200, 200, 200, 255});
            DrawLineV(Vector2{1000 + WindowoffsetX, -200 + WindowoffsetY}, Vector2{1000 + WindowoffsetX, 700 + WindowoffsetY}, (Color){200, 200, 200, 255});
            DrawLineV(Vector2{0 + WindowoffsetX, -200 + WindowoffsetY}, Vector2{0 + WindowoffsetX, 700 + WindowoffsetY}, (Color){200, 200, 200, 255});

            DrawParticles(Particles, NumOfCircles);
        EndDrawing();
    };
    
    return 0;
}