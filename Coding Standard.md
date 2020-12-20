# Project-wide Coding Standard

### 변수

1. 모든 변수는 `= default;`를 붙여 `CS0649 warning`이 발생하지 않게 한다.
2. 모든 `const` 변수는 대문자와 `_`를 사용한다.
3. static 변수는 유틸리티 클래스, 매니저 클래스 외 가급적 사용하지 않는다.

```csharp
public int initialDamage = default; // <- 1
private const int DEFAULT_INITIAL_DAMAGE = 12; // <- 2

// event function from unity
private void Reset() {
	this.initialDamage = DEFAULT_INITIAL_DAMAGE;
}
```



- public 멤버 변수

lowerCamelCase

```csharp
public int currentHealth = default;
```



- serialized private 멤버 변수

m_으로 시작하는 lowerCamelCase

```csharp
[SerializeField] private int m_maxLevel = default;
```



- private 멤버 변수

_으로 시작하는 lowerCamelCase

```csharp
private int _currentLevel = default
```



- public 프로퍼티

UpperCamelCase

```csharp
public int CurrentLevel {get; set;}
```



- private 프로퍼티

원칙적으로 사용하지 않으나 필요시 _UpperCamelCase

```cshar
private int _CurrentLevel {get; set;}
```





### 함수

1. private 함수도 접근제한자를 꼭 사용한다.

2. 함수명은 동사로 시작해야한다.

3. 콜백함수나 델리게이트로만 사용되는 경우 `On메서드명`으로 표기한다.

   ```csharp
   private void OnChangeMonsterType(Enum.MonsterType newType) {
       ...
   }
   ```

   

   

### 인터페이스

1. `IUpperCamelCase`로 네이밍한다.

   ```csharp
   public interface IEquippable {
       bool CanEquip(ICharacter character);
       void Equip(ICharacter character);
   }
   ```

   

### 네임스페이스

1. UpperCamelCase로 네이밍한다.

   ```csharp
   namespace Cqunity.Rhythmical {
       public class GameManager : MonoBehaviour {
           
       }
   }
   ```

2. 빌드에 포함되지 않는 `Editor` 기능의 경우 이름 뒤에 Editor를 붙인다.

   ```csharp
   namespace Cqunity.Rhythmical.Editor {
       class SongGenerator : EditorWindow {
           
       }
   }
   ```

   

### 클래스

1. UpperCamelCase로 네이밍한다.

2. 추상클래스는 Base를 끝에 붙인다.

3. 더 이상 상속받을 수 없는 클래스는 `sealed` 키워드로 추가적인 상속을 막는다.

   ```csharp
   // ItemDefinitionBase.cs
   public abstract class ItemDefinitionBase : ScriptableObject {
       
   }
   
   // WeaponItemDefinition.cs
   public class WeaponItemDefinition : ItemDefinitionBase {
       
   }
   
   // ConsumableItemDefinition.cs
   public class ConsumableItemDefinition : ItemDefinitionBase {
       
   }
   
   // HealthItemDefinition.cs
   public sealed class HealthItemDefinition : ConsumableItemDefinition {
       
   }
   
   ```

   