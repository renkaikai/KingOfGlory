﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill1 : MonoBehaviour
{
    private Hero selectHero;  

    private Skill skill1;
    private int skill1Level = 0;
    public Image skillLevelImage;
    public GameObject addSkill1Image;

    //冷却等级
    private int cdLevel = 0;
    //技能冷却时间
    private float coldTime;
    //技能冷却计时
    private float timer = 0;
    private bool startTimer = false;
    //技能冷却图片
    public Image skillImage;
    public Image skillCdImage;

    private PlayerController pc;
    private Button btn;
    // Use this for initialization
    void Start()
    {
        pc = this.GetPlayerController(0);
        HeroCharacter c=(HeroCharacter)pc.GetCharacter();
        selectHero = c.hero;
        skill1 = selectHero.skills[1];

        //添加技能图标
        skillImage.sprite = skill1.icon;
        skillCdImage.sprite = skill1.icon;
        skillCdImage.fillAmount = 1;
     
        btn = GetComponentInChildren<Button>();
    }

    // Update is called once per frame 
    void Update()
    {
        if (skill1Level > 3)
        {
            addSkill1Image.SetActive(false);
        }
        if (startTimer)
        {
            timer += Time.deltaTime;
            skillCdImage.fillAmount = (coldTime - timer) / coldTime;
        }
        if (timer >= coldTime)
        {
            startTimer = false;
            timer = 0;
            btn.interactable = true;
        }
    }

    //冷却
    public void ColdClick()
    {
        if (skill1Level>0)
        {

        btn.interactable = false;
        skillCdImage.fillAmount = 1;

        coldTime = skill1.cdLevels[cdLevel].value;
        //print("冷却时间" + coldTime);
        startTimer = true;
        cdLevel++;
        //print("冷却等级"+cdLevel);
        if (cdLevel >= skill1.cdLevels.Length)
        {
            //print("冷却满级了");
            cdLevel = skill1.cdLevels.Length - 1;
        }
        //向playerController发送点击了恢复技能的信息
       pc.ReleasingSkills(skill1);

        }
    }

    //技能加点 事件
    public void AddSkillLevelClick()
    {
        if (skill1Level == 0)
        {
            skillCdImage.fillAmount = 0;
            skill1Level++;
        }
        if (skill1Level <= 3 && skill1Level > 0)
        {
            skillLevelImage.fillAmount += 0.33f;
            skill1Level++;
        }
    }
}
