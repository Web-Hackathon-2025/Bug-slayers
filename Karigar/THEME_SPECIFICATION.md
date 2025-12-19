# Karigar - Complete Visual Theme Specification

## 1. Core Identity

**Brand Personality:**
- Professional service marketplace
- Trustworthy and reliable
- Clean, uncluttered aesthetic
- Emphasizes local connection and community
- Modern yet approachable
- Efficient and user-friendly

**Visual Tone:**
- Professional but not corporate
- Warm and inviting
- Clear and accessible
- Consistent and cohesive

---

## 2. Color Palette

### Primary Colors

**Primary Brand Blue (Dominant):**
- `#2563EB` - Primary blue (main brand color)
- `#1E40AF` - Primary blue dark (hover states, emphasis)
- `#3B82F6` - Primary blue light (highlights, accents)
- `#60A5FA` - Primary blue lighter (subtle backgrounds)

**Usage:**
- Primary buttons
- Navigation bar
- Links
- Active states
- Brand elements
- Call-to-action buttons

### Secondary Colors

**Professional Grey:**
- `#1F2937` - Charcoal grey (text, headers)
- `#374151` - Dark grey (secondary text)
- `#4B5563` - Medium grey (borders, dividers)
- `#6B7280` - Light grey (muted text)
- `#9CA3AF` - Lighter grey (disabled states)

**Usage:**
- Body text
- Secondary buttons
- Borders
- Dividers
- Muted information

**Warm Grey (Tertiary):**
- `#F3F4F6` - Light background grey
- `#E5E7EB` - Lighter background grey
- `#D1D5DB` - Border grey

**Usage:**
- Page backgrounds
- Card backgrounds
- Section dividers
- Input backgrounds

### Accent Colors

**Success Green:**
- `#10B981` - Success green
- `#059669` - Success green dark (hover)
- `#D1FAE5` - Success green light (backgrounds)

**Warning Amber:**
- `#F59E0B` - Warning amber
- `#D97706` - Warning amber dark (hover)
- `#FEF3C7` - Warning amber light (backgrounds)

**Error Red:**
- `#EF4444` - Error red
- `#DC2626` - Error red dark (hover)
- `#FEE2E2` - Error red light (backgrounds)

**Info Cyan:**
- `#06B6D4` - Info cyan
- `#0891B2` - Info cyan dark (hover)
- `#CFFAFE` - Info cyan light (backgrounds)

### Text Colors

- **Primary Text:** `#111827` (almost black)
- **Secondary Text:** `#6B7280` (medium grey)
- **Muted Text:** `#9CA3AF` (light grey)
- **Inverse Text:** `#FFFFFF` (white on dark backgrounds)
- **Link Text:** `#2563EB` (primary blue)
- **Link Hover:** `#1E40AF` (primary blue dark)

### Background Colors

- **Page Background:** `#F9FAFB` (very light grey)
- **Card Background:** `#FFFFFF` (white)
- **Section Background:** `#F3F4F6` (light grey)
- **Dark Background:** `#1F2937` (charcoal)
- **Nav Background:** `#2563EB` to `#1E40AF` (gradient)

---

## 3. Typography System

### Font Families

**Primary Font (Headings):**
- Font Family: `-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif`
- Style: Modern, clean, system fonts
- Usage: All headings (H1-H6), navigation, buttons

**Secondary Font (Body):**
- Font Family: `-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif`
- Style: Same system font stack for consistency
- Usage: Body text, paragraphs, form labels

**Monospace (Code/Data):**
- Font Family: `"SF Mono", Monaco, "Cascadia Code", "Roboto Mono", Consolas, monospace`
- Usage: Code snippets, technical data (if needed)

### Font Size Scale

**Headings:**
- H1: `2.5rem` (40px) - Page titles, hero headings
- H2: `2rem` (32px) - Section headings
- H3: `1.75rem` (28px) - Subsection headings
- H4: `1.5rem` (24px) - Card titles, form section headers
- H5: `1.25rem` (20px) - Small section headers
- H6: `1rem` (16px) - Smallest headings

**Body Text:**
- Large Body: `1.125rem` (18px) - Important paragraphs
- Body: `1rem` (16px) - Standard body text
- Small: `0.875rem` (14px) - Captions, helper text
- Extra Small: `0.75rem` (12px) - Fine print, labels

**Special:**
- Display: `3.5rem` (56px) - Hero display text
- Lead: `1.25rem` (20px) - Lead paragraphs

### Font Weights

- **Light:** 300 (rarely used)
- **Regular:** 400 (body text, default)
- **Medium:** 500 (navigation links, buttons)
- **Semibold:** 600 (card titles, form labels)
- **Bold:** 700 (headings, emphasis)
- **Extrabold:** 800 (hero text, special emphasis)

### Line Heights

- **Tight:** 1.25 (headings)
- **Normal:** 1.5 (body text)
- **Relaxed:** 1.75 (long-form content)

### Letter Spacing

- **Tight:** -0.025em (headings)
- **Normal:** 0em (body text)
- **Wide:** 0.05em (uppercase text, labels)
- **Wider:** 0.1em (small caps, badges)

---

## 4. Component Styling Rules

### Buttons

**Primary Button:**
- Background: Gradient from `#2563EB` to `#1E40AF`
- Text Color: `#FFFFFF`
- Padding: `0.625rem 1.25rem` (10px 20px)
- Border Radius: `8px`
- Font Weight: `500`
- Font Size: `1rem`
- Box Shadow: `0 4px 6px -1px rgba(0, 0, 0, 0.1)`
- Hover: Darker gradient, lift `-2px`, stronger shadow
- Active: Pressed effect, slightly darker
- Disabled: `40%` opacity, no hover effects

**Secondary Button:**
- Background: `#4B5563` (medium grey)
- Text Color: `#FFFFFF`
- Border: None
- Hover: `#374151` (darker grey), lift effect
- Same padding, radius, shadow as primary

**Ghost/Outline Button:**
- Background: Transparent
- Text Color: `#2563EB`
- Border: `2px solid #2563EB`
- Hover: Fill with `#2563EB`, text becomes white
- Same padding, radius

**Danger Button:**
- Background: `#EF4444`
- Text Color: `#FFFFFF`
- Hover: `#DC2626`
- Same styling principles as primary

**Success Button:**
- Background: `#10B981`
- Text Color: `#FFFFFF`
- Hover: `#059669`
- Same styling principles as primary

**Button Sizes:**
- Small: `0.375rem 0.75rem`, font-size `0.875rem`
- Medium (default): `0.625rem 1.25rem`, font-size `1rem`
- Large: `0.75rem 1.5rem`, font-size `1.125rem`

### Cards

**Standard Card:**
- Background: `#FFFFFF`
- Border: None
- Border Radius: `12px`
- Box Shadow: `0 4px 6px -1px rgba(0, 0, 0, 0.1)`
- Padding: `1.5rem`
- Hover: Lift `-5px`, shadow increases to `0 20px 25px -5px rgba(0, 0, 0, 0.1)`
- Transition: `all 0.3s ease`

**Card Header:**
- Background: Gradient from `#2563EB` to `#1E40AF`
- Text Color: `#FFFFFF`
- Padding: `1rem 1.5rem`
- Font Weight: `600`
- Border Radius: `12px 12px 0 0`

**Card Body:**
- Padding: `1.5rem`
- Background: `#FFFFFF`

**Card Footer:**
- Padding: `1rem 1.5rem`
- Border Top: `1px solid #E5E7EB`
- Background: `#F9FAFB`

**Elevated Card (Dashboard):**
- Same as standard but with colored left border (`4px solid #2563EB`)
- Hover: Border color darkens, card lifts

### Form Fields

**Input/Textarea/Select:**
- Background: `#FFFFFF`
- Border: `2px solid #D1D5DB`
- Border Radius: `8px`
- Padding: `0.625rem 1rem`
- Font Size: `1rem`
- Color: `#111827`
- Focus: Border `#2563EB`, box-shadow `0 0 0 3px rgba(37, 99, 235, 0.1)`
- Placeholder: `#9CA3AF`
- Disabled: Background `#F3F4F6`, text `#6B7280`

**Label:**
- Font Weight: `500`
- Font Size: `0.875rem`
- Color: `#374151`
- Margin Bottom: `0.5rem`

**Error State:**
- Border Color: `#EF4444`
- Box Shadow: `0 0 0 3px rgba(239, 68, 68, 0.1)`
- Error Text: `#DC2626`, font-size `0.875rem`

**Success State:**
- Border Color: `#10B981`
- Box Shadow: `0 0 0 3px rgba(16, 185, 129, 0.1)`

**Checkbox/Radio:**
- Size: `1.25rem`
- Border: `2px solid #D1D5DB`
- Border Radius: `4px` (checkbox), `50%` (radio)
- Checked: Background `#2563EB`, border `#2563EB`
- Focus: Box shadow `0 0 0 3px rgba(37, 99, 235, 0.1)`

### Navigation

**Navbar:**
- Background: Gradient from `#2563EB` to `#1E40AF`
- Height: `64px`
- Padding: `1rem 0`
- Box Shadow: `0 4px 6px -1px rgba(0, 0, 0, 0.1)`

**Nav Links:**
- Color: `rgba(255, 255, 255, 0.9)`
- Font Weight: `500`
- Padding: `0.5rem 1rem`
- Border Radius: `6px`
- Hover: Background `rgba(255, 255, 255, 0.1)`, color `#FFFFFF`
- Active: Underline effect (2px, animated)

**Nav Brand:**
- Font Size: `1.5rem`
- Font Weight: `700`
- Color: `#FFFFFF`
- Letter Spacing: `0.5px`
- Hover: Slight lift, color `#F3F4F6`

### Rating Stars

**Star Display:**
- Color (filled): `#FBBF24` (amber gold)
- Color (empty): `#D1D5DB` (light grey)
- Font Size: `1.2rem` (for display), `1rem` (for inputs)
- Spacing: `0.125rem` between stars

**Star Input (if interactive):**
- Hover: Scale to `1.2x`
- Active: Scale to `1.1x`
- Transition: `all 0.2s ease`

---

## 5. Visual Properties

### Border Radius Scale

- **None:** `0px` - Sharp corners (rarely used)
- **Small:** `4px` - Small elements, badges
- **Medium:** `8px` - Buttons, inputs, small cards
- **Large:** `12px` - Cards, modals, containers
- **XLarge:** `16px` - Large cards, hero sections
- **Full:** `50%` - Pills, avatars, circular elements

### Box Shadow Levels

**Shadow Small:**
- `0 1px 2px 0 rgba(0, 0, 0, 0.05)`
- Usage: Subtle elevation, borders

**Shadow Medium:**
- `0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06)`
- Usage: Cards, buttons, dropdowns

**Shadow Large:**
- `0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)`
- Usage: Modals, elevated cards, popovers

**Shadow XLarge:**
- `0 20px 25px -5px rgba(0, 0, 0, 0.1), 0 10px 10px -5px rgba(0, 0, 0, 0.04)`
- Usage: Hero sections, major modals

**Shadow Inner:**
- `inset 0 2px 4px 0 rgba(0, 0, 0, 0.06)`
- Usage: Inputs, pressed buttons

### Spacing Rhythm (8px Grid)

**Scale:**
- `0.25rem` (4px) - `spacing-1`
- `0.5rem` (8px) - `spacing-2`
- `0.75rem` (12px) - `spacing-3`
- `1rem` (16px) - `spacing-4` (base unit)
- `1.5rem` (24px) - `spacing-6`
- `2rem` (32px) - `spacing-8`
- `3rem` (48px) - `spacing-12`
- `4rem` (64px) - `spacing-16`
- `6rem` (96px) - `spacing-24`

**Usage:**
- Padding: Use multiples of 4px (0.25rem)
- Margins: Use multiples of 8px (0.5rem) for consistency
- Gaps: Use 1rem (16px) for card grids, 0.5rem for tight spacing

### Icon Style

**Icon Library:** Material Icons or Font Awesome (outline style preferred)

**Icon Sizes:**
- Small: `16px` (inline with text)
- Medium: `24px` (buttons, cards)
- Large: `32px` (feature sections)
- XLarge: `48px` (hero sections)

**Icon Colors:**
- Default: `#6B7280` (medium grey)
- Primary: `#2563EB` (blue)
- Success: `#10B981` (green)
- Warning: `#F59E0B` (amber)
- Error: `#EF4444` (red)
- Inverse: `#FFFFFF` (on dark backgrounds)

**Icon Style:**
- Outline style preferred (not filled)
- Consistent stroke width: `1.5px`
- Rounded corners on geometric shapes

### Image Treatment

**Service Provider Images:**
- Border Radius: `12px`
- Aspect Ratio: `16:9` or `4:3`
- Object Fit: `cover`
- Hover: Slight scale `1.05`, shadow increase

**Avatar Images:**
- Border Radius: `50%` (circular)
- Size: `40px` (small), `64px` (medium), `96px` (large)
- Border: `2px solid #E5E7EB`
- Hover: Border color changes to `#2563EB`

**Thumbnail Images:**
- Border Radius: `8px`
- Aspect Ratio: `1:1` or `4:3`
- Object Fit: `cover`

**Placeholder Images:**
- Background: `#F3F4F6`
- Icon: `#9CA3AF`
- Text: `#6B7280`

---

## 6. Page-Specific Emphases

### Service Listing Pages

**Visual Hierarchy:**
- Search bar: Prominent, card-style container with gradient border on focus
- Filter chips: Rounded pills, `#F3F4F6` background, `#2563EB` when active
- Service cards: Equal height, hover lift effect, clear CTA button
- Availability badge: Green (`#10B981`) with pulsing animation if available now
- Rating display: Large, prominent, with review count
- Price: Bold, `#2563EB` color, larger font size

**Trust Signals:**
- Verified badge: Blue checkmark icon
- Review count: Visible, clickable
- Response time: Small badge, `#10B981` if fast
- Years in business: Subtle text below name

### Provider Profiles

**Trust Emphasis:**
- Profile header: Gradient background (`#2563EB` to `#1E40AF`)
- Rating: Large, centered, with star visualization
- Review count: Prominent, below rating
- Verification badges: Top-right corner, blue background
- Response rate: Progress bar style, green if >90%

**Information Layout:**
- Services list: Card grid, clear pricing
- Reviews section: Expandable, newest first
- Availability calendar: Visual calendar, green for available
- Contact info: Card with icon, prominent CTA button

**Visual Elements:**
- Profile image: Large, circular, with border
- Business name: Large, bold, `#111827`
- Category tag: Colored badge, `#2563EB` background
- Description: Readable line height, `#6B7280` color

### Booking Flow

**Progression Indicators:**
- Step indicator: Horizontal progress bar, `#2563EB` for completed, `#D1D5DB` for pending
- Step numbers: Circular badges, `#2563EB` when active, `#9CA3AF` when inactive
- Step labels: Bold when active, grey when inactive

**Form Sections:**
- Each step: Card container, clear heading
- Required fields: Asterisk, red color
- Validation: Real-time, green checkmark or red X
- Next button: Prominent, full-width, `#2563EB`
- Back button: Ghost style, left-aligned

**Confirmation:**
- Success message: Large checkmark icon, `#10B981`
- Booking details: Card layout, clear hierarchy
- Actions: Primary button for "View Booking", secondary for "Book Another"

### Dashboard Pages

**Information Density:**
- Grid layout: 3-4 columns on desktop, responsive
- Card sizes: Consistent height, varied width based on importance
- Metrics: Large numbers, `#2563EB`, smaller labels below
- Charts/Graphs: Minimal color palette, `#2563EB` primary, `#10B981` for positive

**Quick Actions:**
- Action buttons: Prominent, `#2563EB`, icon + text
- Recent activity: Timeline style, alternating left/right
- Notifications: Badge with count, `#EF4444` background

**Data Tables:**
- Headers: `#1F2937` background, white text
- Rows: Alternating `#FFFFFF` and `#F9FAFB`
- Hover: `#F3F4F6` background, slight lift
- Actions: Icon buttons, `#6B7280`, `#2563EB` on hover

**Status Indicators:**
- Pending: `#F59E0B` (amber), pulsing animation
- Approved: `#10B981` (green), checkmark icon
- Rejected: `#EF4444` (red), X icon
- In Progress: `#06B6D4` (cyan), spinner icon

---

## 7. Animation & Transitions

### Transition Timing

- **Fast:** `0.15s` - Hover states, quick feedback
- **Normal:** `0.3s` - Standard interactions, card hovers
- **Slow:** `0.5s` - Page transitions, modal appearances

### Easing Functions

- **Ease Out:** `cubic-bezier(0.4, 0, 0.2, 1)` - Most interactions
- **Ease In Out:** `cubic-bezier(0.4, 0, 0.2, 1)` - Page transitions
- **Spring:** `cubic-bezier(0.68, -0.55, 0.265, 1.55)` - Playful elements (rare)

### Animation Types

**Fade In:**
- Opacity: `0` to `1`
- Transform: `translateY(20px)` to `translateY(0)`
- Duration: `0.5s`
- Usage: Page loads, card appearances

**Slide In:**
- Transform: `translateX(-20px)` to `translateX(0)`
- Duration: `0.3s`
- Usage: Sidebar, dropdowns

**Scale:**
- Transform: `scale(0.95)` to `scale(1)`
- Duration: `0.2s`
- Usage: Button clicks, modal appearances

**Pulse:**
- Transform: `scale(1)` to `scale(1.05)` and back
- Duration: `2s`, infinite
- Usage: Availability indicators, notifications

---

## 8. Responsive Breakpoints

- **Mobile:** `0px - 640px` (sm)
- **Tablet:** `641px - 1024px` (md)
- **Desktop:** `1025px - 1280px` (lg)
- **Large Desktop:** `1281px+` (xl)

**Mobile Adjustments:**
- Reduced padding: `1rem` instead of `1.5rem`
- Stacked layouts: Single column
- Larger touch targets: Minimum `44px x 44px`
- Simplified navigation: Hamburger menu
- Full-width buttons: Easier tapping

---

## 9. Accessibility Standards

**Color Contrast:**
- Text on white: Minimum `4.5:1` ratio
- Large text (18px+): Minimum `3:1` ratio
- Interactive elements: Minimum `3:1` ratio

**Focus States:**
- Visible outline: `2px solid #2563EB`
- Box shadow: `0 0 0 3px rgba(37, 99, 235, 0.2)`
- Never remove focus indicators

**Touch Targets:**
- Minimum size: `44px x 44px`
- Adequate spacing: `8px` minimum between targets

---

## 10. Implementation Notes

**CSS Variables:**
- All colors should be defined as CSS variables for easy theming
- Use semantic naming: `--color-primary`, `--color-text-primary`, etc.

**Component Consistency:**
- All similar components should share the same styling rules
- Use utility classes for common patterns
- Maintain spacing rhythm throughout

**Performance:**
- Use `transform` and `opacity` for animations (GPU accelerated)
- Minimize layout shifts
- Lazy load images below the fold

**Browser Support:**
- Modern browsers (last 2 versions)
- Graceful degradation for older browsers
- Progressive enhancement approach

---

This specification provides a complete visual foundation for the Karigar marketplace while maintaining the existing functional structure.

